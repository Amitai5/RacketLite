using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class ExpressionNotFoundException : RacketException
    {
        public ExpressionNotFoundException(string expression) 
            : base($"The expression, '{expression}', does not exist.")
        {

        }
    }
}
