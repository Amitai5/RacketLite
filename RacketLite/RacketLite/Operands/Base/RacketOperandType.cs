using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Operands
{
    public enum RacketOperandType
    {
        Any = -1,
        Unknown,
        String,
        Number,
        Boolean,
        Variable,
        Expression,
    }
}
