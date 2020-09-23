using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Operands
{
    public enum OperandType
    {
        Unknown = -1,

        String,
        Number,
        Boolean,
        Variable,
        Expression,
    }
}
