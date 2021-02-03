using System;

namespace RacketLite.Exceptions
{
    public class ContractViolationException : RacketException
    {
        public ContractViolationException(string message)
            : base(message)
        {

        }

        public ContractViolationException(int expectedParamCount, int givenParamCount, bool atLeast = false)
            : base($"Racket contract violation. Expected {(atLeast ? $"at least {expectedParamCount}" : expectedParamCount)} parameter{(expectedParamCount > 1 ? "s" : "")}, given {givenParamCount}")
        {

        }

        public ContractViolationException(Type expectedType, Type? givenType)
            : base($"Racket contract violation. Expected {expectedType.Name}, given {(givenType == null ? "None" : givenType.Name)}")
        {

        }
    }
}
