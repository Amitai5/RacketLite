using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Exceptions
{
    public class VariableNotFoundException : RacketVMException
    {
        public VariableNotFoundException(string variableName) 
            : base($"The variable '{variableName}' does not exist.")
        {

        }
    }
}
