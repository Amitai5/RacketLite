namespace RacketLite.Oporators
{
    public enum RacketOporatorType
    {
        //Special Oporators
        NOP,
        If,
        Define,
        ReturnVariable,
        ReturnConstant,
        ReturnExpression,
        UserDefinedFunction,

        //Numeric Oporators
        Abs,
        Add,
        AddOne,
        ArcCosine,
        ArcSine,
        ArcTangent,
        Ceiling,
        Cosine,
        CurrentSeconds,
        Divide,
        ExponentialPower,
        Exponent,
        Floor,
        HyperbolicSign,
        HyperbolicCosine,
        HyperbolicTangent,
        LogBaseE,
        Modulo,
        Multiply,
        Random,
        Remainder,
        Round,
        Sign,
        Square,
        SquareRoot,
        Subtract,
        SubtractOne,

        //Numeric Comparisons
        Equal,
        LessThan,
        GreaterThan,
        LessThanEqualTo,
        GreaterThanEqualTo,
        CheckZero,

        //Boolean Oporators
        Or,
        And,
        Not,

        //String Oporators
        StringLength,
        StringAppend,
        Substring,

        //String Comparisons
        StringEqual,
        StringLessThan,
        StringGreaterThan,
        StringLessThanEqualTo,
        StringGreaterThanEqualTo,
    }
}