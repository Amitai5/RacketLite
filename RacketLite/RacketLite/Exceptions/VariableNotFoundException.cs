using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class VariableNotFoundException : RacketException
    {
        public VariableNotFoundException(string variableName) 
            : base($"The variable '{variableName}' does not exist.")
        {

        }
    }
}
