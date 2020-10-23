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
            string[] tokenArray = tokens.ToArray();
            OperandQueue operands = new OperandQueue(tokens.Count);
            for (int i = 0; i < tokenArray.Length; i++)
            {
                //Check if token starts with inexact prefix
                bool inexact = tokenArray[i].StartsWith(ParsingRules.InexactNumberPrefix);
                if (inexact)
                {
                    tokenArray[i] = tokenArray[i].Remove(0, ParsingRules.InexactNumberPrefix.Length);
                }

                //Try convert to natural
                if (ulong.TryParse(tokenArray[i], out ulong naturalValue) && naturalValue != 0)
                {
                    operands.Enqueue(new NaturalOperand(naturalValue, inexact));
                }

                //Try convert to integer
                else if (long.TryParse(tokenArray[i], out long integerValue))
                {
                    operands.Enqueue(new IntegerOperand(integerValue, inexact));
                }

                //Try convert to number
                else if (double.TryParse(tokenArray[i], out double numberValue))
                {
                    operands.Enqueue(new NumberOperand(numberValue, inexact));
                }

                //Try convert to string
                else if (tokenArray[i].Count(s => s == '"') == 2)
                {
                    operands.Enqueue(new StringOperand(tokenArray[i].Replace("\"", "")));
                }

                //Try convert to inner expression
                else if (innerExpressions.ContainsKey(tokenArray[i]))
                {
                    operands.Enqueue(innerExpressions[tokenArray[i]]);
                }

                //Try to get variable value of token
                else if (StaticsManager.VariableMap.ContainsKey(tokenArray[i]))
                {
                    operands.Enqueue(StaticsManager.VariableMap[tokenArray[i]]);
                }

                //Try to get racket constant value of token
                else if (StaticsManager.RacketConstants.ContainsKey(tokenArray[i]))
                {
                    operands.Enqueue(StaticsManager.RacketConstants[tokenArray[i]]);
                }

                //At last resort add the token as an Unkown Operand (if valid)
                else if (!ContainsInvalidCharacter(tokenArray[i]))
                {
                    operands.Enqueue(new UnknownOperand(tokenArray[i]));
                }

                //If the token is using an invalid character, throw error
                else
                {
                    throw new InvalidNameException(tokenArray[i]);
                }
            }
            return operands.ToArray();
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

        public static Queue<string> ParseExpressionTokens(string parsedExpressionText)
        {
            bool seenQuote = false;
            Queue<string> tokenQueue = new Queue<string>();
            StringBuilder currentToken = new StringBuilder();
            foreach(char currentChar in parsedExpressionText)
            {
                currentToken.Append(currentChar);

                //Check for strings
                if (currentChar == '"')
                {
                    if (!seenQuote)
                    {
                        seenQuote = true;
                    }
                    else
                    {
                        tokenQueue.Enqueue(currentToken.ToString().Trim());
                        currentToken.Clear();
                        seenQuote = false;
                    }
                }
                else if (currentChar == ' ' && !seenQuote && !string.IsNullOrWhiteSpace(currentToken.ToString()))
                {
                    tokenQueue.Enqueue(currentToken.ToString().Trim());
                    currentToken.Clear();
                }
            }

            //Add the last token if not empty
            if(!string.IsNullOrWhiteSpace(currentToken.ToString()))
            {
                tokenQueue.Enqueue(currentToken.ToString().Trim());
            }
            return tokenQueue;
        }

        public static (RacketOporator, bool) ParseRacketOporator(string racketOporatorName)
        {
            if (OporatorDefinitions.RacketOporatorMap.ContainsKey(racketOporatorName))
            {
                return (OporatorDefinitions.RacketOporatorMap[racketOporatorName], false);
            }
            else if (StaticsManager.UserDefinedOporators.ContainsKey(racketOporatorName))
            {
                return (StaticsManager.UserDefinedOporators[racketOporatorName], false);
            }
            else if (StaticsManager.VariableMap.ContainsKey(racketOporatorName))
            {
                return (new RacketOporator(RacketOporatorType.ReturnVariable, null, 1, 1, RacketOperandType.Any), true);
            }
            else if (StaticsManager.RacketConstants.ContainsKey(racketOporatorName))
            {
                return (new RacketOporator(RacketOporatorType.ReturnConstant, null, 1, 1, RacketOperandType.Any), true);
            }
            return (null, true);
        }

        private static bool ContainsInvalidCharacter(string unknownToken)
        {
            for (int i = 0; i < ParsingRules.InvalidCharacters.Length; i++)
            {
                if (unknownToken.StartsWith(ParsingRules.InvalidCharacters[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
