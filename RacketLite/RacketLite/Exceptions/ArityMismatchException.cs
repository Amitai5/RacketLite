using RacketLite.Oporators;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class ArityMismatchException : RacketException
    {
        public ArityMismatchException(RacketOporatorType opCode, int argCount, int actual)
            : base($"The function, ({opCode.ToString().ToLower()} ...), expects {argCount} argument(s) but recieved {actual}.")
        {

        }
    }
}
