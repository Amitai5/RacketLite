using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class DefinitionOverrideException : RacketException
    {
        public DefinitionOverrideException(string oporatorName) 
            : base($"'{oporatorName}' is already defined")
        {
        }
    }
}
