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
        HyperbolicSine,
        HyperbolicCosine,
        HyperbolicTangent,
        LogBaseE,
        Modulo,
        Multiply,
        Random,
        Remainder,
        Round,
        Sign,
        Sine,
        Square,
        SquareRoot,
        Subtract,
        SubtractOne,
        Tangent,

        //Numeric Comparisons
        Equal,
        LessThan,
        GreaterThan,
        LessThanEqualTo,
        GreaterThanEqualTo,
        IsEven,
        IsInteger,
        IsNegative,
        IsNumber,
        IsOdd,
        IsPositive,
        IsZero,

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