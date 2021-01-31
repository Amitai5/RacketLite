namespace RacketLite.Exceptions
{
    public class UndefinedOperatorException : RacketException
    {
        public UndefinedOperatorException(string opCode) 
            : base($"'{opCode}' undefined; cannot reference an identifier before its definition")
        {
        }
    }
}
