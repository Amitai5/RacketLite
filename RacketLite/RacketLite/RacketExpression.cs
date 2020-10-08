using RacketLite.Oporators;
using RacketLite.Parsing;
using RacketLite.Operands;
using System.Collections.Generic;
using System.Linq;
using RacketLite.Exceptions;
using System.Collections;
using System;

namespace RacketLite
{
    public class RacketExpression : IOperable
    {
        public string Name { get; set; }
        public OperandQueue Operands { get; protected set; }
        public RacketOporator Oporator { get; protected set; }

        public string RacketOporatorSignature { get; }
        public OperandQueue UDEOperands { get; private set; }
        protected List<string> LocalVarNames = new List<string>();
        protected Dictionary<string, RacketExpression> InnerExpressions;

        public RacketExpression(string expressionText)
            : base(RacketOperandType.Expression)
        {
            //Check for empty expression
            if (expressionText == null)
            {
                return;
            }
            else if (expressionText.Trim() == "")
            {
                throw new OporatorNotFoundException("");
            }

            //Clean up the expressionText after getting inner expressions
            InnerExpressions = ExpressionParser.ParseInnerExpressions(ref expressionText);
            expressionText = expressionText.Replace("(", ")").Replace(")", "");
            expressionText = expressionText.Replace("  ", " ").Trim();

            //Create tokens
            Queue<string> tokenStrings = new Queue<string>(expressionText.Split(' ').ToList());
            Operands = new OperandQueue(tokenStrings.Count);

            //Evaluate Racket oporator for expression
            RacketOporatorSignature = tokenStrings.Dequeue();
            Oporator = ExpressionParser.ParseRacketOporator(RacketOporatorSignature);

            //Special Check for a Define Oporator
            if (Oporator != null && Oporator.Type == RacketOporatorType.Define && InnerExpressions.Count > 0)
            {
                string firstExpressionKey = $"{ParsingRules.ExpressionPerface}0";
                RacketExpression firstExpression = InnerExpressions[firstExpressionKey];
                if (firstExpression.Oporator == null)
                {
                    //Add the parameters to the new user function
                    do
                    {
                        string paramName = firstExpression.Operands.Dequeue().GetUnknownValue().ToString();
                        LocalVarNames.Add(paramName);
                    } while (firstExpression.Operands.Count > 0);

                    tokenStrings.Dequeue(); //Remove the function header from the tokens
                    InnerExpressions.Remove(firstExpressionKey);
                    Operands.Enqueue(new StringOperand(firstExpression.RacketOporatorSignature));
                }
                else
                {
                    //Trying to define the same function twice
                    //TODO: MAKE ERROR
                }
            }

            //Create opernds
            Operands.Enqueue(ExpressionParser.ParseTokensAsOperands(tokenStrings, InnerExpressions));
        }

        private DynamicOperand EvaluateExpression(OperandQueue operands)
        {
            switch (Oporator.Type)
            {
                //Special Oporators
                case RacketOporatorType.NOP:
                    return StaticsManager.VariableMap[RacketOporatorSignature];
                case RacketOporatorType.Define:
                    if (LocalVarNames.Count > 0)
                    {
                        string newOpCode = operands.Dequeue().GetStringValue();
                        UserDefinedOporator newOporator = new UserDefinedOporator(newOpCode, LocalVarNames.Count);
                        RacketExpression expression = ((RacketExpression)operands.Dequeue().OperableValue);
                        StaticsManager.userDefinedOporators.Add(newOpCode, newOporator);

                        UserDefinedExpression newExpression = new UserDefinedExpression(expression.Oporator, expression.Operands, LocalVarNames, expression.InnerExpressions);
                        StaticsManager.UserDefinedExpressions.Add(newOpCode, newExpression);
                    }
                    else
                    {
                        DynamicOperand varNameOperand = operands.Dequeue();
                        string varName = varNameOperand.GetUnknownValue().ToString();
                        if (!StaticsManager.VariableMap.ContainsKey(varName))
                        {
                            StaticsManager.VariableMap.Add(varName, operands.Dequeue());
                        }
                    }
                    return null;
                case RacketOporatorType.UserDefinedFunction:
                    return StaticsManager.UserDefinedExpressions[RacketOporatorSignature].Evaluate(operands);

                //Numeric Oporators
                case RacketOporatorType.Multiply:
                    return operands.Dequeue() * operands.Dequeue();
                case RacketOporatorType.Divide:
                    return operands.Dequeue() / operands.Dequeue();
                case RacketOporatorType.Add:
                    return operands.Dequeue() + operands.Dequeue();
                case RacketOporatorType.Subtract:
                    return operands.Dequeue() - operands.Dequeue();

                //Numeric Comparisons
                case RacketOporatorType.Equal:
                    bool result = operands.Dequeue().Equals(operands.Dequeue());
                    return new DynamicOperand(new BooleanOperand(result));
                case RacketOporatorType.LessThan:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 < val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() < operands.Dequeue();
                case RacketOporatorType.GreaterThan:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 > val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() > operands.Dequeue();
                case RacketOporatorType.LessThanEqualTo:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 <= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() <= operands.Dequeue();
                case RacketOporatorType.GreaterThanEqualTo:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 <= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() >= operands.Dequeue();

                //Boolean Oporators
                case RacketOporatorType.Or:
                    return operands.Dequeue() | operands.Dequeue();
                case RacketOporatorType.Not:
                    return !operands.Dequeue();
                case RacketOporatorType.And:
                    return operands.Dequeue() & operands.Dequeue();

                //String Oporators
                case RacketOporatorType.StringAppend:
                    string tempString = operands.Dequeue().GetStringValue() + operands.Dequeue().GetStringValue();
                    return new DynamicOperand(new StringOperand(tempString));
                case RacketOporatorType.Substring:
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

                //String Comparisons
                case RacketOporatorType.StringEqual:
                    bool stringResult = operands.Dequeue().Equals(operands.Dequeue());
                    return new DynamicOperand(new BooleanOperand(stringResult));
                case RacketOporatorType.StringLessThan:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 < val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() < operands.Dequeue();
                case RacketOporatorType.StringGreaterThan:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 > val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() > operands.Dequeue();
                case RacketOporatorType.StringLessThanEqualTo:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 <= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() <= operands.Dequeue();
                case RacketOporatorType.StringGreaterThanEqualTo:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 >= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() >= operands.Dequeue();

                //In case it does not yet defined, throw an error
                default:
                    throw new NotImplementedException($"The oporator, {Oporator}, has not been implemented...");
            }
        }

        public DynamicOperand Evaluate()
        {
            //Check if Oporator exists
            if (Oporator == null)
            {
                throw new OporatorNotFoundException(RacketOporatorSignature);
            }

            //Check for valid expression
            if (UDEOperands != null)
            {
                Oporator.IsValidExpression(UDEOperands);
            }
            else
            {
                Oporator.IsValidExpression(Operands);
            }

            //Check if we are inside a User Defined Expression
            if (UDEOperands != null && UDEOperands.Count > 0)
            {
                do
                {
                    DynamicOperand newOperand = EvaluateExpression(UDEOperands);
                    if (UDEOperands == null)
                    {
                        //In a few cases we will lose our operand copy mid run...
                        return newOperand;
                    }

                    UDEOperands.AddLast(newOperand);
                } while (UDEOperands.Count > 1);

                DynamicOperand result = UDEOperands.Dequeue();
                UDEOperands = null;
                return result;
            }
            else
            {
                do
                {
                    DynamicOperand newOperand = EvaluateExpression(Operands);
                    Operands.AddLast(newOperand);
                } while (Operands.Count > 1);
                return Operands.Dequeue();
            }
        }

        protected static void SetUserDefinedOperands(RacketExpression racketExpression, Dictionary<string, DynamicOperand> localVarValues)
        {
            if (racketExpression.UDEOperands == null || racketExpression.UDEOperands.Count == 0)
            {
                racketExpression.UDEOperands = racketExpression.Operands.ReplaceUnknowns(localVarValues);
            }

            //Run on the inner expressions too
            if (racketExpression.InnerExpressions != null)
            {
                foreach (KeyValuePair<string, RacketExpression> valuePair in racketExpression.InnerExpressions)
                {
                    if (valuePair.Value.UDEOperands == null)
                    {
                        SetUserDefinedOperands(valuePair.Value, localVarValues);
                    }
                }
            }
        }

        public override int CompareTo(object obj)
        {
            throw new Exception("You should never be trying to compare expressions");
        }
    }
}
