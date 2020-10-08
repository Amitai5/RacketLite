using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class OporatorNotFoundException : RacketException
    {
        public OporatorNotFoundException(string opCode) 
            : base($"'{opCode}' is undefined.")
        {

        }
    }
}
