using RacketLite.Exceptions;
using RacketLite.Operands;
using RacketLite.Oporators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RacketLite.Parsing
{
    public static class ExpressionParser
    {
        public static DynamicOperand[] ParseTokensAsOperands(Queue<string> tokens, Dictionary<string, RacketExpression> innerExpressions)
        {
            OperandQueue operands = new OperandQueue(tokens.Count);
            foreach (string token in tokens)
            {
                //Try convert to number
                if (double.TryParse(token, out double numberValue))
                {
                    operands.Enqueue(new NumericOperand(numberValue));
                }

                //Try convert to string
                else if (token.Count(s => s == '"') == 2)
                {
                    operands.Enqueue(new StringOperand(token.Replace("\"", "")));
                }

                //Try convert to inner expression
                else if (innerExpressions.ContainsKey(token))
                {
                    operands.Enqueue(innerExpressions[token]);
                }

                //Try to get variable value of token
                else if (StaticsManager.VariableMap.ContainsKey(token))
                {
                    operands.Enqueue(StaticsManager.VariableMap[token]);
                }

                //Try to get racket constant value of token
                else if (StaticsManager.RacketConstants.ContainsKey(token))
                {
                    operands.Enqueue(StaticsManager.RacketConstants[token]);
                }

                //At last resort add the token as an Unkown Operand
                else
                {
                    operands.Enqueue(new UnknownOperand(token));
                }
            }
            return operands.ToArray();
        }

        public static (RacketOporator, bool) ParseRacketOporator(string racketOporatorName)
        {
            if (OporatorDefinitions.RacketOporatorMap.ContainsKey(racketOporatorName))
            {
                return (OporatorDefinitions.RacketOporatorMap[racketOporatorName], false);
            }
            else if (StaticsManager.UserDefinedExpressions.ContainsKey(racketOporatorName))
            {
                return (StaticsManager.userDefinedOporators[racketOporatorName], false);
            }
            else if (StaticsManager.VariableMap.ContainsKey(racketOporatorName))
            {
                return (new RacketOporator(RacketOporatorType.ReturnVariable, 1, 1, RacketOperandType.Any), true);
            }
            else if (StaticsManager.RacketConstants.ContainsKey(racketOporatorName))
            {
                return (new RacketOporator(RacketOporatorType.ReturnConstant, 1, 1, RacketOperandType.Any), true);
            }
            return (null, true);
        }

        public static Dictionary<string, RacketExpression> ParseInnerExpressions(ref string expressionText)
        {
            int startPos = -1;
            int parenthesisCount = 0;
            Dictionary<string, RacketExpression> innerExpressions = new Dictionary<string, RacketExpression>();
            for (int i = 1; i < expressionText.Length - 1; i++)
            {
                if (expressionText[i] == '(')
                {
                    if (startPos == -1)
                    {
                        //Only set start pos when looking for first-level inner expressions
                        startPos = i;
                    }
                    parenthesisCount++;
                }
                else if (expressionText[i] == ')' && startPos != -1)
                {
                    //Look for outer close parenthesis
                    if (parenthesisCount > 1)
                    {
                        parenthesisCount--;
                        continue;
                    }

                    string newExpressionText = expressionText[startPos..(i + parenthesisCount)];
                    string newExpressionName = $"{ParsingRules.ExpressionPerface}{innerExpressions.Count}";

                    RacketExpression innerExpression = new RacketExpression(newExpressionText)
                    {
                        Name = newExpressionName
                    };

                    expressionText = expressionText.Replace(newExpressionText, $"{newExpressionName} ");
                    i = expressionText.IndexOf(newExpressionName) + newExpressionName.Length - 1;
                    innerExpressions.Add(newExpressionName, innerExpression);
                    parenthesisCount = 0;
                    startPos = -1;
                }
            }
            return innerExpressions;
        }
    }
}
