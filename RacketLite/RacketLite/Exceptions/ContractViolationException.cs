using System;

namespace RacketLite.Exceptions
{
    public class ContractViolationException : RacketException
    {
        public ContractViolationException(Type expectedType, Type? givenType)
            : base($"Racket contract violation. Expected {expectedType.Name}, given {(givenType == null ? "None" : givenType.Name)}")
        {

        }
    }
}
