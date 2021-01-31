using System;

namespace RacketLite.Exceptions
{
    public abstract class RacketException : Exception
    {
        protected RacketException(string? message) : base(message)
        {

        }
    }
}
