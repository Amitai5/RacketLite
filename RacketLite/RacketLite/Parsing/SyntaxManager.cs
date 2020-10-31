using RacketLite.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RacketLite.Parsing
{
    public static class SyntaxManager
    {
        public static void CheckExpressionSyntax(string expressionText)
        {
            //Check parenthesis balance
            if (expressionText.ParenthesisBalance() != 0)
            {
                bool missingClose = expressionText.ParenthesisBalance() == 1;
                throw new UnexpectedParenthesisException(missingClose);
            }

            //Check for balanced quotes
            int quoteCount = expressionText.Count(x => x == '\"');
            if (quoteCount % 2 != 0)
            {
                throw new UnexpectedQuoteException();
            }
        }

        public static int ParenthesisBalance(this string expressionText)
        {
            int openParenthesis = expressionText.Count(x => x == '(');
            int closeParenthesis = expressionText.Count(x => x == ')');
            return openParenthesis.CompareTo(closeParenthesis);
        }
    }
}
