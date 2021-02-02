using System;

namespace RacketLite.Exceptions
{
    public class ContractViolationException : RacketException
    {
        public ContractViolationException(string message)
            : base(message)
        {

        }

        public ContractViolationException(int expectedParamCount, int givenParamCount) 
            : base($"Racket contract violation. Expected {expectedParamCount} parameters, given {givenParamCount}")
        {

        }

        public ContractViolationException(Type expectedType, Type? givenType)
            : base($"Racket contract violation. Expected {expectedType.Name}, given {(givenType == null ? "None" : givenType.Name)}")
        {

        }
    }
}
