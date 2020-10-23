using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class InvalidNameException : RacketException
    {
        public InvalidNameException(string name)
            : base($"The name, '{name}', is invalid.")
        {

        }
    }
}
