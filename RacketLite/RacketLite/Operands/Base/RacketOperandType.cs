using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Operands
{
    [Flags]
    public enum RacketOperandType
    {
        Any = -1,
        Unknown = 1,

        Number = 2,
        Integer = 4,
        Natural = 8,
        String = 16,
        Boolean = 32,
        Variable = 64,
        Expression = 128,
    }
}