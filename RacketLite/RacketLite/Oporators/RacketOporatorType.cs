namespace RacketLite.Oporators
{
    public enum RacketOporatorType
    {
        NOP = 0,
        If,
        Define,
        ReturnVariable,
        FunctionHeader,
        CreateFunction,
        UserDefinedFunction,

        Multiply = 16,
        Divide,
        Add,
        Subtract,
        Equal,
        LessThan,
        GreaterThan,
        LessThanEqualTo,
        GreaterThanEqualTo,

        Or = 32,
        And,
        Not,

        StringEqual = 64,
        StringAppend,
        Substring,
        StringLength,
        StringLessThan,
        StringGreaterThan,
        StringLessThanEqualTo,
        StringGreaterThanEqualTo,
    }
}