using RacketLite.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RacketLite.Parsing
{
    public class SyntaxManager
    {
        public static void CheckExpressionSyntax(string expressionText)
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
    }
}
