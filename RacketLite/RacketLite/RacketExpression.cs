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

        public string ExpressionText { get; }
        public string RacketOporatorSignature { get; }
        private List<string> LocalVarNames = new List<string>();

        private RacketExpression(RacketOporator oporator, OperandQueue operands, List<string> localVarNames)
        {
            Operands = operands;
            Oporator = oporator;
            LocalVarNames = localVarNames;
        }

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
            ExpressionText = expressionText;
            SyntaxManager.CheckExpressionSyntax(expressionText);

            //Clean up the expressionText after getting inner expressions
            Dictionary<string, RacketExpression> innerExpressions = ExpressionParser.ParseInnerExpressions(ref expressionText);
            string parsedExpressionText = expressionText.Replace("(", ")").Replace(")", "");
            parsedExpressionText = parsedExpressionText.Replace("  ", " ").Trim();

            //Create tokens
            Queue<string> tokenStrings = ExpressionParser.ParseExpressionTokens(parsedExpressionText);
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
            if (Oporator != null && Oporator.Type == RacketOporatorType.Define && innerExpressions.Count == 2)
            {
                //Ensure that expression is defined as a function
                Operands.Enqueue(new BooleanOperand(true));

                string firstExpressionKey = $"{ParsingRules.ExpressionPerface}0";
                RacketExpression firstExpression = innerExpressions[firstExpressionKey];
                if (firstExpression.Oporator == null)
                {
                    //Add the parameters to the new user function (skip the last one)
                    while (firstExpression.Operands.Count > 1)
                    {
                        string paramName = firstExpression.Operands.Dequeue().GetUnknownValue().ToString();
                        LocalVarNames.Add(paramName);
                    }

                    tokenStrings.Dequeue(); //Remove the function header from the tokens
                    innerExpressions.Remove(firstExpressionKey);
                    Operands.Enqueue(new StringOperand(firstExpression.RacketOporatorSignature));
                }
                else
                {
                    //Trying to define the same function twice
                    throw new DefinitionOverrideException(firstExpression.RacketOporatorSignature);
                }
            }
            else if (Oporator != null && Oporator.Type == RacketOporatorType.Define)
            {
                //All other cases, define expression as variable
                Operands.Enqueue(new BooleanOperand(false));
            }

            //Create opernds
            Operands.Enqueue(ExpressionParser.ParseTokensAsOperands(tokenStrings, innerExpressions));

            //Ensure all expressions contain at least one set of parenthesis
            if (Oporator != null && !expressionText.Contains('('))
            {
                if (Oporator.Type != RacketOporatorType.ReturnConstant && Oporator.Type != RacketOporatorType.ReturnVariable)
                {
                    Oporator = new RacketOporator(RacketOporatorType.ReturnExpression, null, 1, 1, RacketOperandType.Any);
                    Operands.Enqueue(new StringOperand(RacketOporatorSignature));
                }
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
                case RacketOporatorType.If:
                    bool condition = operands.Dequeue().GetBooleanValue();
                    if (condition)
                    {
                        DynamicOperand trueReturnValue = operands.Dequeue();
                        operands.Dequeue(operands.Count);
                        return trueReturnValue;
                    }
                    return operands.Dequeue(operands.Count);
                case RacketOporatorType.Define:
                    bool isUDF = operands.Dequeue().GetBooleanValue();
                    if (isUDF)
                    {
                        string newOpCode = operands.Dequeue().GetStringValue();
                        UserDefinedOporator newOporator = new UserDefinedOporator(newOpCode, LocalVarNames.Count);
                        RacketExpression expression = ((RacketExpression)operands.Dequeue().OperableValue);
                        expression.LocalVarNames = LocalVarNames;

                        StaticsManager.UserDefinedOporators.Add(newOpCode, newOporator);
                        StaticsManager.AddUDE(newOpCode, expression);
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
                    UserDefinedOporator userDefinedOporator = (UserDefinedOporator)Oporator;
                    RacketExpression udExpression = StaticsManager.GetUDE(userDefinedOporator.DefinitionString);
                    udExpression.SetFunctionLocals(operands);
                    return udExpression.Evaluate();
                #endregion Special Oporators

                #region Numeric Oporators
                case RacketOporatorType.Abs:
                    double absValue = Math.Abs(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(absValue, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Add:
                    double sum = operands.Dequeue().GetDoubleValue() + operands.Dequeue().GetDoubleValue();
                    return new NumberOperand(sum, StaticsManager.StackContainsInexact);
                case RacketOporatorType.AddOne:
                    return new NumberOperand(operands.Dequeue().GetDoubleValue() + 1, StaticsManager.StackContainsInexact);
                case RacketOporatorType.ArcCosine:
                    double arcCosine = Math.Acos(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(arcCosine, true);
                case RacketOporatorType.ArcSine:
                    double arcSine = Math.Asin(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(arcSine, true);
                case RacketOporatorType.ArcTangent:
                    double arcTangent = Math.Atan(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(arcTangent, true);
                case RacketOporatorType.Ceiling:
                    long ceiling = Convert.ToInt64(Math.Ceiling(operands.Dequeue().GetDoubleValue()));
                    return new IntegerOperand(ceiling, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Cosine:
                    double cosine = Math.Cos(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(cosine, true);
                case RacketOporatorType.CurrentSeconds:
                    double currentSecondsDouble = Math.Round((DateTime.UtcNow - DateTime.MinValue).TotalSeconds);
                    long currentSeconds = Convert.ToInt64(currentSecondsDouble);
                    return new IntegerOperand(currentSeconds, false);
                case RacketOporatorType.Divide:
                    double divisionQuotient = operands.Dequeue().GetDoubleValue() / operands.Dequeue().GetDoubleValue();
                    return new NumberOperand(divisionQuotient, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Equal:
                    bool result = operands.Dequeue().Equals(operands.Dequeue());
                    return new BooleanOperand(result);
                case RacketOporatorType.Exponential:
                    double exp = Math.Exp(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(exp, true);
                case RacketOporatorType.Exponent:
                    double exponent = Math.Pow(operands.Dequeue().GetDoubleValue(), operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(exponent, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Floor:
                    long floor = Convert.ToInt64(Math.Floor(operands.Dequeue().GetDoubleValue()));
                    return new IntegerOperand(floor, StaticsManager.StackContainsInexact);
                case RacketOporatorType.GreaterThan:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 > val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() > operands.Dequeue();
                case RacketOporatorType.GreaterThanOrEqual:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 <= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() >= operands.Dequeue();
                case RacketOporatorType.GreatestCommonDivisor:
                    long findGCD(long a, long b)
                    {
                        if (b == 0)
                        {
                            return a;
                        }
                        return findGCD(b, a % b);
                    }
                    long[] gcdValues = operands.Select(x => x.GetLongValue()).ToArray();
                    operands.Dequeue(operands.Count);
                    return new IntegerOperand(gcdValues.Aggregate(findGCD), StaticsManager.StackContainsInexact);
                case RacketOporatorType.HyperbolicCosine:
                    double hypCosine = Math.Cosh(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(hypCosine, true);
                case RacketOporatorType.HyperbolicSine:
                    double hypSine = Math.Sinh(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(hypSine, true);
                case RacketOporatorType.IsEven:
                    bool isEven = operands.Dequeue().GetLongValue() % 2 == 0;
                    return new BooleanOperand(isEven);
                case RacketOporatorType.IsInteger:
                    bool isInteger = operands.Dequeue().Type == RacketOperandType.Integer;
                    return new BooleanOperand(isInteger);
                case RacketOporatorType.IsNegative:
                    bool isNegative = operands.Dequeue().GetDoubleValue() < 0;
                    return new BooleanOperand(isNegative);
                case RacketOporatorType.IsNumber:
                    bool isNumber = operands.Dequeue().Type == RacketOperandType.Number;
                    return new BooleanOperand(isNumber);
                case RacketOporatorType.IsOdd:
                    bool isOdd = operands.Dequeue().GetLongValue() % 2 == 1;
                    return new BooleanOperand(isOdd);
                case RacketOporatorType.IsPositive:
                    bool isPositive = operands.Dequeue().GetDoubleValue() > 0;
                    return new BooleanOperand(isPositive);
                case RacketOporatorType.IsRational:
                    if (operands.Peek().Type != RacketOperandType.Number && operands.Peek().Type != RacketOperandType.Integer
                        && operands.Peek().Type != RacketOperandType.Natural)
                    {
                        operands.Dequeue(operands.Count);
                        return new BooleanOperand(false);
                    }
                    if (operands.Peek().Type == RacketOperandType.Number
                        && ((NumberOperand)operands.Peek().OperableValue).Irrational)
                    {
                        operands.Dequeue(operands.Count);
                        return new BooleanOperand(false);
                    }
                    else if (operands.Peek().Type == RacketOperandType.Integer
                        && ((IntegerOperand)operands.Peek().OperableValue).Inexact)
                    {
                        operands.Dequeue(operands.Count);
                        return new BooleanOperand(false);
                    }
                    else if(operands.Peek().Type == RacketOperandType.Natural
                        && ((NaturalOperand)operands.Peek().OperableValue).Inexact)
                    {
                        operands.Dequeue(operands.Count);
                        return new BooleanOperand(false);
                    }
                    operands.Dequeue(operands.Count);
                    return new BooleanOperand(true);
                case RacketOporatorType.IsZero:
                    return new BooleanOperand(operands.Dequeue().GetDoubleValue() == 0);
                case RacketOporatorType.LessThan:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 < val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() < operands.Dequeue();
                case RacketOporatorType.LessThanOrEqual:
                    if (operands.Count > 2)
                    {
                        DynamicOperand val1 = operands.Dequeue();
                        DynamicOperand val2 = operands.Dequeue();
                        return (val1 <= val2).GetBooleanValue() == true ? val1 : val2;
                    }
                    return operands.Dequeue() <= operands.Dequeue();
                case RacketOporatorType.LeastCommonMultiple:
                    long findLCM(long a, long b)
                    {
                        return Math.Abs(a * b) / findGCD(a, b);
                    }
                    long[] lcmValues = operands.Select(x => x.GetLongValue()).ToArray();
                    operands.Dequeue(operands.Count);
                    return new IntegerOperand(lcmValues.Aggregate(findLCM), StaticsManager.StackContainsInexact);
                case RacketOporatorType.Maximum:
                    double[] maxValArray = operands.Select(x => x.GetDoubleValue()).ToArray();
                    operands.Dequeue(operands.Count);
                    return new NumberOperand(maxValArray.Max(), StaticsManager.StackContainsInexact);
                case RacketOporatorType.Minimum:
                    double[] minValArray = operands.Select(x => x.GetDoubleValue()).ToArray();
                    operands.Dequeue(operands.Count);
                    return new NumberOperand(minValArray.Min(), StaticsManager.StackContainsInexact);
                case RacketOporatorType.Modulo:
                case RacketOporatorType.Remainder:
                    long modulo = operands.Dequeue().GetLongValue() % operands.Dequeue().GetLongValue();
                    return new IntegerOperand(modulo, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Multiply:
                    double product = operands.Dequeue().GetDoubleValue() * operands.Dequeue().GetDoubleValue();
                    return new NumberOperand(product, StaticsManager.StackContainsInexact);
                case RacketOporatorType.NaturalLog:
                    double logE = Math.Log(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(logE, true);
                case RacketOporatorType.Quotient:
                    long quotient = operands.Dequeue().GetLongValue() / operands.Dequeue().GetLongValue();
                    return new IntegerOperand(quotient, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Random:
                    double randValueDouble = new Random().NextDouble();
                    double roundedRandValue = Math.Round(randValueDouble * operands.Dequeue().GetNaturalValue());
                    long randValue = Convert.ToInt64(roundedRandValue);
                    return new IntegerOperand(randValue, false);
                case RacketOporatorType.Round:
                    long roundedValue = Convert.ToInt64(Math.Round(operands.Dequeue().GetDoubleValue()));
                    return new IntegerOperand(roundedValue, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Sine:
                    double sine = Math.Sin(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(sine, true);
                case RacketOporatorType.Sign:
                    double signValue = Math.Sign(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(signValue, false);
                case RacketOporatorType.Square:
                    double squaredValue = Math.Pow(operands.Dequeue().GetDoubleValue(), 2);
                    return new NumberOperand(squaredValue, StaticsManager.StackContainsInexact);
                case RacketOporatorType.SquareRoot:
                    double squareRootValue = Math.Sqrt(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(squareRootValue, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Subtract:
                    double difference = operands.Dequeue().GetDoubleValue() - operands.Dequeue().GetDoubleValue();
                    return new NumberOperand(difference, StaticsManager.StackContainsInexact);
                case RacketOporatorType.SubtractOne:
                    return new NumberOperand(operands.Dequeue().GetDoubleValue() - 1, StaticsManager.StackContainsInexact);
                case RacketOporatorType.Tangent:
                    double tangent = Math.Tan(operands.Dequeue().GetDoubleValue());
                    return new NumberOperand(tangent, true);
                #endregion Numeric Oporators

                #region Number Conversion
                case RacketOporatorType.ExactToInexact:
                    return new NumberOperand(operands.Dequeue().GetDoubleValue(), true);
                case RacketOporatorType.InexactToExact:
                    return new NumberOperand(operands.Dequeue().GetDoubleValue(), false);
                case RacketOporatorType.NumberToString:
                    return new StringOperand(operands.Dequeue().GetDoubleValue().ToString());
                #endregion

                #region Boolean Oporators
                case RacketOporatorType.And:
                    bool leftAndValue = operands.Dequeue().GetBooleanValue();
                    bool rightAndValue = operands.Dequeue().GetBooleanValue();
                    return new BooleanOperand(leftAndValue && rightAndValue);
                case RacketOporatorType.BooleanEqual:
                    bool booleanEqual = operands.Dequeue().GetBooleanValue() == operands.Dequeue().GetBooleanValue();
                    return new BooleanOperand(booleanEqual);
                case RacketOporatorType.IsBoolean:
                    return new BooleanOperand(operands.Dequeue().Type == RacketOperandType.Boolean);
                case RacketOporatorType.IsFalse:
                    if(operands.Peek().Type != RacketOperandType.Boolean)
                    {
                        operands.Dequeue(operands.Count);
                        return new BooleanOperand(false);
                    }
                    return new BooleanOperand(operands.Dequeue().GetBooleanValue() == false);
                case RacketOporatorType.Not:
                    bool notValue = operands.Dequeue().GetBooleanValue();
                    return new BooleanOperand(!notValue);
                case RacketOporatorType.Or:
                    bool leftOrValue = operands.Dequeue().GetBooleanValue();
                    bool rightOrValue = operands.Dequeue().GetBooleanValue();
                    return new BooleanOperand(leftOrValue || rightOrValue);
                #endregion

                #region Boolean Conversions
                case RacketOporatorType.BooleanToInteger:
                    long boolAsLong = Convert.ToInt64(operands.Dequeue().GetBooleanValue());
                    return new IntegerOperand(boolAsLong, false);
                case RacketOporatorType.BooleanToString:
                    return new StringOperand(operands.Dequeue().GetBooleanValue().ToString().ToLower());
                #endregion

                #region String Oporators
                case RacketOporatorType.StringLength:
                    int stringLength = operands.Dequeue().GetStringValue().Length;
                    return new IntegerOperand(stringLength, false);
                case RacketOporatorType.StringAppend:
                    string tempString = operands.Dequeue().GetStringValue() + operands.Dequeue().GetStringValue();
                    return new StringOperand(tempString);
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
                    return new StringOperand(stringPart);
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

            //Replace locals except on define
            if (Oporator.Type != RacketOporatorType.Define)
            {
                ReplaceFunctionLocal(this);
            }

            //Check for valid expression
            Oporator.IsValidExpression(Operands);

            //Check which operands to use
            do
            {
                DynamicOperand newOperand = EvaluateExpression(Operands);
                Operands.AddLast(newOperand);
            } while (Operands.Count > 1);

            //Check for inexact as products of the expression
            if (Operands.ContainsInexact())
            {
                StaticsManager.StackContainsInexact = true;
            }

            //Return the result
            DynamicOperand result = Operands.Dequeue();
            return result;
        }

        #region User Definied Expressions
        private void SetFunctionLocals(OperandQueue localVarValues)
        {
            //Create var name/value map
            for (int i = 0; i < LocalVarNames.Count; i++)
            {
                DynamicOperand varValue = localVarValues.Dequeue();
                if (!StaticsManager.LocalStack.ContainsKey(LocalVarNames[i]))
                {
                    StaticsManager.LocalStack.Add(LocalVarNames[i], varValue);
                }
                else if (varValue.Type != RacketOperandType.Unknown)
                {
                    StaticsManager.LocalStack[LocalVarNames[i]] = varValue;
                }
            }
        }

        private static void ReplaceFunctionLocal(RacketExpression racketExpression)
        {
            //Replace operands for the expression
            racketExpression.Operands.ReplaceUnknowns(StaticsManager.LocalStack);

            //Check for inexact in user-set operands
            if (racketExpression.Operands.ContainsInexact())
            {
                StaticsManager.StackContainsInexact = true;
            }

            //Run on the inner expressions too
            foreach (DynamicOperand operand in racketExpression.Operands)
            {
                if (operand.Type == RacketOperandType.Expression)
                {
                    ReplaceFunctionLocal(((RacketExpression)operand.OperableValue));
                }
            }
        }
        #endregion User Defined Expressions

        /// <summary>
        /// Creates a fresh copy of the racket expression
        /// </summary>
        /// <returns>The copy of the expression as a new instance</returns>
        public RacketExpression GetCopy()
        {
            RacketExpression innerExpression = new RacketExpression(ExpressionText);
            return new RacketExpression(Oporator, innerExpression.Operands, LocalVarNames);
        }

        public override int CompareTo(object obj)
        {
            DynamicOperand thisValue = Evaluate();
            return thisValue.CompareTo(obj);
        }
    }
}