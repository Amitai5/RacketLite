using RacketVM.Oporator;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Exceptions
{
    public class ArgumentMismatchException : RacketVMException
    {
        public ArgumentMismatchException(RacketOporator opCode, int argCount, int actual)
            : base($"The function, ({opCode.ToString().ToLower()} ...), expects {argCount} argument(s) but recieved {actual}.")
        {

        }
    }
}
