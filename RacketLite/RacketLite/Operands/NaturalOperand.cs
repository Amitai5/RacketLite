using System;

namespace RacketLite.Operands
{
    public class NaturalOperand : IOperable
    {
        public long OperandValue;

        public NaturalOperand(long value)
            : base(RacketOperandType.Natural)
        {
            OperandValue = value;
        }

        #region Object Overrides

        public override bool Equals(object obj)
        {
            return OperandValue == ((NaturalOperand)obj).OperandValue;
        }

        public override int CompareTo(object obj)
        {
            double otherValue = ((NaturalOperand)obj).OperandValue;
            return OperandValue.CompareTo(otherValue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, OperandValue);
        }

        #endregion Object Overrides
    }
}
