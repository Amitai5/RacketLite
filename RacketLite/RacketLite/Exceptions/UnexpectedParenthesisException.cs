using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class UnexpectedParenthesisException : RacketException
    {
        public UnexpectedParenthesisException(bool missingClose)
            : base($"{(missingClose ? "Missing ')'" : "Unexpected ')' found")} in expression")
        {

        }
    }
}
