using RacketLite.Operands;
using System;

namespace RacketLite.Operands
{
    public abstract class IOperable : IComparable
    {
        public RacketOperandType Type { get; protected set; }

        public IOperable(RacketOperandType type = RacketOperandType.Unknown)
        {
            Type = type;
        }

        public abstract int CompareTo(object obj);
    }
}
