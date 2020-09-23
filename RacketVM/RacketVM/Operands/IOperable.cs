using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Operands
{
    public abstract class IOperable : IComparable
    {
        public OperandType Type { get; protected set; }

        public IOperable(OperandType type = OperandType.Unknown)
        {
            Type = type;
        }

        public abstract int CompareTo(object obj);
    }
}
