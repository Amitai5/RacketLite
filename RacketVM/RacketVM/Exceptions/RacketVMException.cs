using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Exceptions
{
    public abstract class RacketVMException : Exception
    {
        public RacketVMException(string message)
            : base(message)
        {

        }
    }
}
