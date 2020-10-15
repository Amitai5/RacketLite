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
            string parsedExpressionText = expressionText.Replace("(", ")").Replace(")", "");
            parsedExpressionText = parsedExpressionText.Replace("  ", " ").Trim();

            //Create tokens
            Queue<string> tokenStrings = new Queue<string>(parsedExpressionText.Split(' ').ToList());
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
                //Ensure that expression is defined as a function
                Operands.Enqueue(new BooleanOperand(true));

                string firstExpressionKey = $"{ParsingRules.ExpressionPerface}0";
                RacketExpression firstExpression = InnerExpressions[firstExpressionKey];
                if (firstExpression.Oporator == null)
                {
                    //Add the parameters to the new user function (skip the last one)
                    while (firstExpression.Operands.Count > 1)
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
            else if(Oporator != null && Oporator.Type == RacketOporatorType.Define)
            {
                //All other cases, define expression as variable
                Operands.Enqueue(new BooleanOperand(false));
            }

            //Create opernds
            Operands.Enqueue(ExpressionParser.ParseTokensAsOperands(tokenStrings, InnerExpressions));

            //Check if user typed in a constant number or string
            if (Oporator == null && Operands.Count == 1)
            {
                RacketOperandType operandType = Operands.Peek().Type;
                if (operandType != RacketOperandType.Unknown && operandType != RacketOperandType.Expression)
                {
                    Oporator = new RacketOporator(RacketOporatorType.ReturnConstant, 1, 1, RacketOperandType.Any);
                }
            }

            //Ensure all expressions contain at least one set of parenthesis
            else if (Oporator != null && !expressionText.Contains('(')) //TODO: Make sure that return variable does not hit this
            {
                Oporator = new RacketOporator(RacketOporatorType.ReturnExpression, 1, 1, RacketOperandType.Any);
                Operands.Enqueue(new StringOperand(RacketOporatorSignature));
            }
        }

        private DynamicOperand EvaluateExpression(OperandQueue operands)
        {
            switch (Oporator.Type)
            {
                #region Special Oporators
                case RacketOporatorType.ReturnVariable:
                case RacketOporatorType.ReturnConstant:
                    return operands.Dequeue();
                case RacketOporatorType.ReturnExpression:
                    string procedureName = operands.Dequeue(operands.Count).GetStringValue();
                    return new StringOperand($"#<procedure:{procedureName}>");
                case RacketOporatorType.Define:
                    bool isUDF = operands.Dequeue().GetBooleanValue();
                    if (isUDF)
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
                #endregion Special Oporators

                #region Numeric Oporators
                case RacketOporatorType.Abs:
                    double absValue = Math.Abs(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(absValue);
                case RacketOporatorType.Add:
                    return operands.Dequeue() + operands.Dequeue();
                case RacketOporatorType.AddOne:
                    return new NumberOperand(operands.Dequeue().GetDoubleValue() + 1);
                case RacketOporatorType.ArcCosine:
                    double arcCosine = Math.Acos(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(arcCosine);
                case RacketOporatorType.ArcSine:
                    double arcSine = Math.Asin(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(arcSine);
                case RacketOporatorType.ArcTangent:
                    double arcTangent = Math.Atan(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(arcTangent);
                case RacketOporatorType.Ceiling:
                    long ceiling = Convert.ToInt64(Math.Ceiling(operands.Dequeue().GetDoubleValue()));
                    return new NaturalOperand(ceiling);
                case RacketOporatorType.Cosine:
                    double cosine = Math.Cos(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(cosine);
                case RacketOporatorType.CurrentSeconds:
                    double currentSecondsDouble = Math.Round((DateTime.UtcNow - DateTime.MinValue).TotalSeconds);
                    long currentSeconds = Convert.ToInt64(currentSecondsDouble);
                    return new NaturalOperand(currentSeconds);
                case RacketOporatorType.Divide:
                    return operands.Dequeue() / operands.Dequeue();
                case RacketOporatorType.ExponentialPower:
                    double exp = Math.Exp(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(exp);
                case RacketOporatorType.Exponent:
                    double exponent = Math.Pow(operands.Dequeue().GetDoubleValue(), operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(exponent);
                case RacketOporatorType.Floor:
                    long floor = Convert.ToInt64(Math.Floor(operands.Dequeue().GetDoubleValue()));
                    return new NaturalOperand(floor);
                case RacketOporatorType.HyperbolicCosine:
                    double hypCosine = Math.Cosh(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(hypCosine);
                case RacketOporatorType.HyperbolicSign:
                    double hypSine = Math.Sinh(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(hypSine);
                case RacketOporatorType.HyperbolicTangent:
                    double hypTangent = Math.Tanh(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(hypTangent);
                case RacketOporatorType.LogBaseE:
                    double logE = Math.Log(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(logE);
                case RacketOporatorType.Modulo:
                case RacketOporatorType.Remainder:
                    long modulo = operands.Dequeue().GetLongValue() % operands.Dequeue().GetLongValue();
                    return new NaturalOperand(modulo);
                case RacketOporatorType.Multiply:
                    return operands.Dequeue() * operands.Dequeue();
                case RacketOporatorType.Random:
                    double randValueDouble = new Random().NextDouble();
                    double roundedRandValue = Math.Round(randValueDouble * operands.Dequeue().GetLongValue());
                    long randValue = Convert.ToInt64(roundedRandValue);
                    return new NaturalOperand(randValue);
                case RacketOporatorType.Round:
                    long roundedValue = Convert.ToInt64(Math.Round(operands.Dequeue().GetDoubleValue()));
                    return new NaturalOperand(roundedValue);
                case RacketOporatorType.Sign:
                    double signValue = Math.Sign(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(signValue);
                case RacketOporatorType.Square:
                    double squaredValue = Math.Pow(operands.Dequeue().GetDoubleValue(), 2);
                    return new NumberOperand(squaredValue);
                case RacketOporatorType.SquareRoot:
                    double squareRootValue = Math.Sqrt(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(squareRootValue);
                case RacketOporatorType.Subtract:
                    return operands.Dequeue() - operands.Dequeue();
                case RacketOporatorType.SubtractOne:
                    return new NumberOperand(operands.Dequeue().GetDoubleValue() - 1);
                #endregion Numeric Oporators

                #region Numeric Comparisons
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
                #endregion Numeric Comparisons

                #region String Oporators
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
                #endregion String Oporators

                #region String Comparisons
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
                #endregion String Comparisons

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
