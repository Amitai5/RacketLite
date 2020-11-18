using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class LocalDefinedVariableNotFound : RacketException
    {
        public LocalDefinedVariableNotFound()
            : base("Local excpected a define as the first expression")
        {

        }
    }
}
