using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Operands
{
    public enum RacketOperandType
    {
        Any = -1,
        Unknown = 0,

        Number = 1,
        Natural = 2,
        String = 3,
        Boolean = 4,
        Variable = 5,
        Expression = 6,
    }
}