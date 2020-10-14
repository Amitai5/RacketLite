using RacketLite.Oporators;
using RacketLite.Parsing;
using RacketLite.Operands;
using System.Collections.Generic;
using System.Linq;
using RacketLite.Exceptions;
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

            //Check syntax of expression
            CheckExpressionSyntax(expressionText);

            //Clean up the expressionText after getting inner expressions
            InnerExpressions = ExpressionParser.ParseInnerExpressions(ref expressionText);
            expressionText = expressionText.Replace("(", ")").Replace(")", "");
            expressionText = expressionText.Replace("  ", " ").Trim();

            //Create tokens
            Queue<string> tokenStrings = new Queue<string>(expressionText.Split(' ').ToList());
            Operands = new OperandQueue(tokenStrings.Count);

            //Evaluate Racket oporator for expression
            RacketOporatorSignature = tokenStrings.Dequeue();
            (RacketOporator, bool) oporatorValue = ExpressionParser.ParseRacketOporator(RacketOporatorSignature);
            Oporator = oporatorValue.Item1;

            //Check if oporator is a operand itself
            if (oporatorValue.Item2)
            {
                tokenStrings.Enqueue(RacketOporatorSignature);
            }

            //Special Check for a Define Oporator
            if (Oporator != null && Oporator.Type == RacketOporatorType.Define && InnerExpressions.Count == 2)
            {
                string firstExpressionKey = $"{ParsingRules.ExpressionPerface}0";
                RacketExpression firstExpression = InnerExpressions[firstExpressionKey];
                if (firstExpression.Oporator == null)
                {
                    //Add a single null to differentiate between definition of var and function
                    LocalVarNames.Add(null);

                    //Add the parameters to the new user function
                    while (firstExpression.Operands.Count > 0)
                    {
                        string paramName = firstExpression.Operands.Dequeue().GetUnknownValue().ToString();
                        LocalVarNames.Add(paramName);
                    }

                    tokenStrings.Dequeue(); //Remove the function header from the tokens
                    InnerExpressions.Remove(firstExpressionKey);
                    Operands.Enqueue(new StringOperand(firstExpression.RacketOporatorSignature));
                }
                else
                {
                    //Trying to define the same function twice
                    throw new DefinitionOverrideException(firstExpression.RacketOporatorSignature);
                }
            }

            //Create opernds
            Operands.Enqueue(ExpressionParser.ParseTokensAsOperands(tokenStrings, InnerExpressions));

            //Check if user typed in a constant value
            if (Oporator == null && Operands.Count == 1)
            {
                RacketOperandType operandType = Operands.Peek().Type;
                if (operandType != RacketOperandType.Unknown && operandType != RacketOperandType.Expression)
                {
                    Oporator = new RacketOporator(RacketOporatorType.ReturnConstant, 1, 1, RacketOperandType.Any);
                }
            }
        }

        private DynamicOperand EvaluateExpression(OperandQueue operands)
        {
            switch (Oporator.Type)
            {
                //Special Oporators
                case RacketOporatorType.ReturnVariable:
                case RacketOporatorType.ReturnConstant:
                    return operands.Dequeue();
                case RacketOporatorType.Define:
                    if (LocalVarNames.Count > 0)
                    {
                        string newOpCode = operands.Dequeue(1).GetStringValue(); //throw out the extra null
                        UserDefinedOporator newOporator = new UserDefinedOporator(newOpCode, LocalVarNames.Count - 1);
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
                            DynamicOperand varValue = operands.Dequeue();
                            if (varValue.Type == RacketOperandType.Expression)
                            {
                                StaticsManager.VariableMap.Add(varName, varValue.GetExpressionValue());
                            }
                            else
                            {
                                StaticsManager.VariableMap.Add(varName, varValue);
                            }
                        }
                    }
                    return null;
                case RacketOporatorType.UserDefinedFunction:
                    return StaticsManager.UserDefinedExpressions[RacketOporatorSignature].Evaluate(operands);

                #region Numeric Oporators
                case RacketOporatorType.Abs:
                    double absValue = Math.Abs(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(absValue);
                case RacketOporatorType.Add:
                    return operands.Dequeue() + operands.Dequeue();
                case RacketOporatorType.AddOne:
                    return new NumericOperand(operands.Dequeue().GetDoubleValue() + 1);
                case RacketOporatorType.ArcCosine:
                    double arcCosine = Math.Acos(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(arcCosine);
                case RacketOporatorType.ArcSine:
                    double arcSine = Math.Asin(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(arcSine);
                case RacketOporatorType.ArcTangent:
                    double arcTangent = Math.Atan(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(arcTangent);
                case RacketOporatorType.Ceiling:
                    double ceiling = Math.Ceiling(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(ceiling);
                case RacketOporatorType.Cosine:
                    double cosine = Math.Cos(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(cosine);
                case RacketOporatorType.Divide:
                    return operands.Dequeue() / operands.Dequeue();
                case RacketOporatorType.ExponentialPower:
                    double exp = Math.Exp(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(exp);
                case RacketOporatorType.Exponent:
                    double exponent = Math.Pow(operands.Dequeue().GetDoubleValue(), operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(exponent);
                case RacketOporatorType.Floor:
                    double floor = Math.Floor(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(floor);
                case RacketOporatorType.HyperbolicCosine:
                    double hypCosine = Math.Cosh(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(hypCosine);
                case RacketOporatorType.HyperbolicSign:
                    double hypSine = Math.Sinh(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(hypSine);
                case RacketOporatorType.HyperbolicTangent:
                    double hypTangent = Math.Tanh(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(hypTangent);
                case RacketOporatorType.LogBaseE:
                    double logE = Math.Log(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(logE);
                case RacketOporatorType.Modulo:
                case RacketOporatorType.Remainder:
                    double modulo = operands.Dequeue().GetDoubleValue() % operands.Dequeue().GetDoubleValue();
                    return new NumericOperand(modulo);
                case RacketOporatorType.Multiply:
                    return operands.Dequeue() * operands.Dequeue();
                case RacketOporatorType.Random:
                    int randValue = new Random().Next((int)operands.Dequeue().GetDoubleValue()); //TODO: NATURALS
                    return new NumericOperand(randValue);
                case RacketOporatorType.Round:
                    double roundedValue = Math.Round(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(roundedValue);
                case RacketOporatorType.Sign:
                    double signValue = Math.Sign(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(signValue);
                case RacketOporatorType.Square:
                    double squaredValue = Math.Pow(operands.Dequeue().GetDoubleValue(), 2);
                    return new NumericOperand(squaredValue);
                case RacketOporatorType.SquareRoot:
                    double squareRootValue = Math.Sqrt(operands.Dequeue().GetDoubleValue());
                    return new NumericOperand(squareRootValue);
                case RacketOporatorType.Subtract:
                    return operands.Dequeue() - operands.Dequeue();
                case RacketOporatorType.SubtractOne:
                    return new NumericOperand(operands.Dequeue().GetDoubleValue() - 1);
                #endregion Numeric Oporators

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
                case RacketOporatorType.CheckZero:
                    return new BooleanOperand(operands.Dequeue().GetDoubleValue() == 0);

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

        private void CheckExpressionSyntax(string expressionText)
        {
            //Check parenthesis balance
            int openParenthesis = expressionText.Count(x => x == '(');
            int closeParenthesis = expressionText.Count(x => x == ')');
            if (openParenthesis != closeParenthesis)
            {
                bool missingClose = openParenthesis > closeParenthesis;
                throw new UnexpectedParenthesisException(missingClose);
            }

            //Check for balanced quotes
            int quoteCount = expressionText.Count(x => x == '\"');
            if (quoteCount % 2 != 0)
            {
                throw new UnexpectedQuoteException();
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
            DynamicOperand thisValue = Evaluate();
            return thisValue.CompareTo(obj);
        }
    }
}
