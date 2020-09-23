using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Oporator
{
    public enum RacketOporator
    {
        NOP = 0,
        If,
        Define,
        FunctionHeader,
        CreateFunction,
        UserDefinedFunction,

        Multiply = 16,
        Divide,
        Add,
        Subtract,

        Or = 32,
        And,
        Not,
        Equal,
        LessThan,
        GreaterThan,
        LessThanEqualTo,
        GreaterThanEqualTo,

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
