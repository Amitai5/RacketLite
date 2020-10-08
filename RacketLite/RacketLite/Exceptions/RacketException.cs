using System;

namespace RacketLite.Exceptions
{
    public abstract class RacketException : Exception
    {
        public RacketException(string message)
            : base(message)
        {

        }
    }
}
