using RacketVM.Exceptions;
using RacketVM.Oporator;
using RacketVM.Operands.Variables;
using System.Collections.Generic;
using RacketVM.Operands;
using System.Linq;

namespace RacketVM
{
    public class Expression : IOperable
    {
        private Queue<DynamicOperand> operands;
        public IOperable[] Operands { get => operands.ToArray(); }

        public readonly string ExpressionText;
        public ExpressionOporator Oporator { get; }

        private Expression ParentExpression { get; }
        private Dictionary<string, DynamicOperand> LocalVarMap = new Dictionary<string, DynamicOperand>();

        public Expression(string expressionText, Expression parent)
            : base(OperandType.Expression)
        {
            //Create base objects
            operands = new Queue<DynamicOperand>();
            ExpressionText = expressionText;
            ParentExpression = parent;

            //Get OpCode
            if (ExpressionText.Trim().Contains(" "))
            {
                string expressionOpCode = ExpressionText[1..ExpressionText.IndexOf(" ")];
                if (OporatorManager.OporatorDefinitions.ContainsKey(expressionOpCode))
                {
                    RacketOporator racketOporator = OporatorManager.OporatorDefinitions[expressionOpCode];
                    Oporator = new ExpressionOporator(racketOporator);
                }
                else if (UserDefinedOporators.Instance.Contains(expressionOpCode))
                {
                    Oporator = UserDefinedOporators.Instance.GetOporator(expressionOpCode);
                }
                else if (ParentExpression != null && ParentExpression.Oporator.OporatorType != RacketOporator.Define)
                {
                    Oporator = new ExpressionOporator(RacketOporator.FunctionHeader);
                }
                else
                {
                    //TODO: GOOD ERROR
                }
            }
            else
            {
                Oporator = new ExpressionOporator(RacketOporator.NOP);
            }

            //Oporate on define expression
            int startPos = 1;
            if (Oporator.OporatorType == RacketOporator.Define)
            {
                //Check for defined function
                string[] parts = ExpressionText.Split(" ");
                if (parts[1].StartsWith("("))
                {
                    Oporator = new ExpressionOporator(RacketOporator.FunctionHeader);
                    operands.Enqueue(new StringOperand(parts[1][1..].Trim()));
                }
                else
                {
                    operands.Enqueue(new StringOperand(parts[1]));
                }
                startPos = ExpressionText.IndexOf(parts[1]);
            }

            //Check for inner expressions etc
            for (int i = startPos; i < ExpressionText.Length; i++)
            {
                if (ExpressionText[i] == '(')
                {
                    for (int j = i + 1; j < ExpressionText.Length - 1; j++)
                    {
                        //Check for double-nested functions
                        if (ExpressionText[j] == '(')
                        {
                            j = ExpressionText.IndexOf(')', j);
                        }
                        else if (ExpressionText[j] == ')')
                        {
                            int startIndex = ExpressionText.IndexOf("(", i);
                            string innerExpressionText = ExpressionText[startIndex..(j + 1)];

                            //Find and evaluate the funciton header
                            Expression innerExpression = new Expression(innerExpressionText, this);
                            if (innerExpression.Oporator.OporatorType == RacketOporator.FunctionHeader)
                            {
                                innerExpression.Evaluate();
                                Oporator.OporatorType = RacketOporator.CreateFunction;
                            }
                            else if (GetOldestParent(this).Oporator.OporatorType != RacketOporator.CreateFunction)
                            {
                                operands.Enqueue(innerExpression);
                            }
                            i = j;
                        }
                    }
                }

                //Check for constants and defined variables
                if (ExpressionText[i].ToString() == " ")
                {
                    int endIndex = ExpressionText.IndexOf(" ", i + 1);
                    if (endIndex == -1)
                    {
                        endIndex = ExpressionText.IndexOf(")", i + 1);
                    }

                    //Find parameter name
                    string paramName = ExpressionText[i..endIndex].Replace(")", "").Trim();

                    //Check if we are in a function header
                    if (Oporator.OporatorType == RacketOporator.FunctionHeader)
                    {
                        string expressionOpCode = ExpressionText[1..ExpressionText.IndexOf(" ")];
                        string newParamName = $"udf_{expressionOpCode}_{paramName}_{ExpressionDepth(this)}";

                        //Localize parameters to each individual function
                        GetOldestParent(this).Oporator.LocalizedVarNames.Add(paramName, newParamName);
                        operands.Enqueue(new StringOperand(newParamName));

                        //Re-calculate end index now
                        endIndex = ExpressionText.IndexOf(" ", i + 1) - 1;
                        if (endIndex <= -1)
                        {
                            endIndex = ExpressionText.IndexOf(")", i + 1) - 1;
                        }

                        //Ship over the variable name
                        i = endIndex;
                    }
                    else if (GetOldestParent(this).Oporator.OporatorType != RacketOporator.CreateFunction)
                    {
                        //Check variable type
                        if (double.TryParse(paramName, out double variableValue))
                        {
                            operands.Enqueue(new NumericOperand(variableValue));
                        }
                        else if (paramName.StartsWith("\""))
                        {
                            int startStringIndex = ExpressionText.IndexOf(paramName, i) + 1;
                            int endStringIndex = ExpressionText.IndexOf("\"", startStringIndex);
                            paramName = ExpressionText[startStringIndex..endStringIndex];
                            string varValue = paramName.Replace("\"", "");
                            operands.Enqueue(new StringOperand(varValue));

                            //Move past the string
                            i = endStringIndex;
                        }
                        else if (!paramName.Contains("("))
                        {
                            if (ConstantManager.ConstantDefinitions.ContainsKey(paramName))
                            {
                                operands.Enqueue(ConstantManager.ConstantDefinitions[paramName]);
                            }
                            else if (GetOldestParent(this).LocalVarMap.ContainsKey(paramName))
                            {
                                operands.Enqueue(GetOldestParent(this).LocalVarMap[paramName]);
                            }
                            else
                            {
                                operands.Enqueue(new Variable(paramName));
                            }
                        }
                    }
                }
            }

            //Add the 'new' function body
            if (Oporator.OporatorType == RacketOporator.CreateFunction)
            {
                int startIndex = ExpressionText.IndexOf(")", 1) + 1;
                operands.Enqueue(new StringOperand(ExpressionText[startIndex..^1].Trim()));
            }
        }

        public DynamicOperand EvaluateExpression(Queue<DynamicOperand> operands)
        {
            switch (Oporator.OporatorType)
            {
                //Special Oporators
                case RacketOporator.NOP:
                    Variable variable = new Variable(ExpressionText.Trim());
                    return variable.Evaluate();
                case RacketOporator.Define:
                    string varName = operands.Dequeue().GetStringValue();
                    VariableManager.AddVar(varName, operands.Dequeue());
                    return null;
                case RacketOporator.If:
                    DynamicOperand expressionValue = operands.Dequeue();
                    if (expressionValue.GetBooleanValue())
                    {
                        return operands.Dequeue().EvaluateDynamic();
                    }
                    return expressionValue;
                case RacketOporator.FunctionHeader:
                    {
                        ExpressionOporator parentOporator = GetOldestParent(this).Oporator;
                        for (int i = 0; i < operands.Count; i++)
                        {
                            parentOporator.UserOperandNames.Add(operands.Dequeue().GetStringValue());
                        }
                    }
                    break;
                case RacketOporator.CreateFunction:
                    {
                        string opCode = operands.Dequeue().GetStringValue();
                        string expressionText = operands.Dequeue().GetStringValue();
                        Dictionary<string, string> localizedVars = GetOldestParent(this).Oporator.LocalizedVarNames;
                        string[] origionalParamNames = localizedVars.Keys.ToArray();

                        for (int i = 0; i < localizedVars.Count; i++)
                        {
                            string paramName = origionalParamNames[i];
                            expressionText = expressionText.Replace(paramName, localizedVars[paramName]);
                        }

                        Oporator.UpdateExpressionText(expressionText);
                        Oporator.OporatorType = RacketOporator.UserDefinedFunction;

                        UserDefinedOporators.Instance.AddOporator(opCode, Oporator);
                    }
                    return null;
                case RacketOporator.UserDefinedFunction:
                    {
                        Expression OldestParentExpression = GetOldestParent(this);
                        OldestParentExpression.LocalVarMap.Add(Oporator.UserOperandNames[0], operands.Dequeue());
                        for (int i = 1; i < operands.Count; i++)
                        {
                            OldestParentExpression.LocalVarMap.Add(Oporator.UserOperandNames[i], operands.Dequeue());
                        }
                        Expression newExpression = new Expression(Oporator.ExpressionText, this);
                        return newExpression.Evaluate();
                    }

                //Numeric Oporators
                case RacketOporator.Multiply:
                    return operands.Dequeue() * operands.Dequeue();
                case RacketOporator.Divide:
                    return operands.Dequeue() / operands.Dequeue();
                case RacketOporator.Add:
                    return operands.Dequeue() + operands.Dequeue();
                case RacketOporator.Subtract:
                    return operands.Dequeue() - operands.Dequeue();

                //Boolean Oporators
                case RacketOporator.Or:
                    return operands.Dequeue() | operands.Dequeue();
                case RacketOporator.Not:
                    return !operands.Dequeue();
                case RacketOporator.And:
                    return operands.Dequeue() & operands.Dequeue();
                case RacketOporator.Equal:
                    ThrowOnStrings(operands.Dequeue(), operands.Dequeue());
                    bool result = operands.Dequeue().Equals(operands.Dequeue());
                    return new DynamicOperand(new BooleanOperand(result));
                case RacketOporator.LessThan:
                    ThrowOnStrings(operands.Peek(), operands.Peek());
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 < val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() < operands.Dequeue();
                case RacketOporator.GreaterThan:
                    ThrowOnStrings(operands.Peek(), operands.Peek());
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 > val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() > operands.Dequeue();
                case RacketOporator.LessThanEqualTo:
                    ThrowOnStrings(operands.Peek(), operands.Peek());
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 <= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() <= operands.Dequeue();
                case RacketOporator.GreaterThanEqualTo:
                    ThrowOnStrings(operands.Peek(), operands.Peek());
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 <= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() >= operands.Dequeue();

                //String Oporators
                case RacketOporator.StringAppend:
                    string tempString = operands.Dequeue().GetStringValue() + operands.Dequeue().GetStringValue();
                    return new DynamicOperand(new StringOperand(tempString));
                case RacketOporator.Substring:
                    string stringPart = operands.Dequeue().GetStringValue();
                    if (operands.Peek() != null)
                    {
                        stringPart = stringPart.Substring((int)operands.Dequeue().GetDoubleValue());
                    }
                    else
                    {
                        stringPart = stringPart.Substring((int)operands.Dequeue().GetDoubleValue(), (int)operands.Dequeue().GetDoubleValue());
                    }
                    return new DynamicOperand(new StringOperand(stringPart));

                case RacketOporator.StringEqual:
                    bool stringResult = operands.Dequeue().Equals(operands.Dequeue());
                    return new DynamicOperand(new BooleanOperand(stringResult));
                case RacketOporator.StringLessThan:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 < val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() < operands.Dequeue();
                case RacketOporator.StringGreaterThan:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 > val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() > operands.Dequeue();
                case RacketOporator.StringLessThanEqualTo:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 <= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() <= operands.Dequeue();
                case RacketOporator.StringGreaterThanEqualTo:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 >= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() >= operands.Dequeue();
            }
            return null;
        }

        public DynamicOperand Evaluate()
        {
            //Check operand count
            if (operands.Count < Oporator.OperandMin)
            {
                throw new ArgumentMismatchException(Oporator.OporatorType, Oporator.OperandMin, operands.Count);
            }
            else if (operands.Count > Oporator.OperandMax)
            {
                throw new ArgumentMismatchException(Oporator.OporatorType, Oporator.OperandMin, operands.Count);
            }

            //Loop until we use up all operands
            do
            {
                //Calculate the new part
                DynamicOperand newOperand = EvaluateExpression(operands);

                if (!Oporator.KeepVariablePosition)
                {
                    operands.Enqueue(newOperand);
                }
                else
                {
                    //Add the new operand at the top of the list
                    List<DynamicOperand> operandList = new List<DynamicOperand>() { newOperand };
                    operandList.AddRange(operands.ToList());
                    operands = new Queue<DynamicOperand>(operandList);
                }
            } while (operands.Count > 1);
            return operands.Dequeue();
        }

        private static Expression GetOldestParent(Expression expression)
        {
            if (expression.ParentExpression == null)
            {
                return expression;
            }
            else
            {
                return GetOldestParent(expression.ParentExpression);
            }
        }

        private static int ExpressionDepth(Expression expression)
        {
            static int findDepth(Expression exp, int index)
            {
                if (exp.ParentExpression == null)
                {
                    return index + 1;
                }
                else
                {
                    return findDepth(exp.ParentExpression, index + 1);
                }
            }
            return findDepth(expression, 0);
        }

        private void ThrowOnStrings(DynamicOperand previousResult, DynamicOperand operand)
        {
            if (previousResult.Type == OperandType.String || operand.Type == OperandType.String)
            {
                throw new TypeConversionException(OperandType.String, OperandType.Number);
            }
        }

        public override int CompareTo(object obj)
        {
            DynamicOperand otherValue = ((Expression)obj).Evaluate();
            return Evaluate().CompareTo(otherValue);
        }
    }
}
