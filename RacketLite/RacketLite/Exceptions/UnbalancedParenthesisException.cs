namespace RacketLite.Exceptions
{
    public class UnbalancedParenthesisException : RacketException
    {
        public UnbalancedParenthesisException(bool missingClose)
            : base($"read-syntax: {(missingClose ? "missing" : "unexpected")} ')'")
        {
        }
    }
}