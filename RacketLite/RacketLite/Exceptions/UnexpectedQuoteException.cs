using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class UnexpectedQuoteException : RacketException
    {
        public UnexpectedQuoteException() 
            : base("Unexpected \" found in expression")
        {
        }
    }
}
