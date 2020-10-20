using System;

namespace RacketLite.Operands
{
    public class NumberOperand : IOperable
    {
        public double OperandValue;

        public NumberOperand(double value)
            : base(RacketOperandType.Number)
        {
            OperandValue = value;
        }

        #region IOperable Overrides
        public override bool Equals(object obj)
        {
            return OperandValue == ((NumberOperand)obj).OperandValue;
        }

        public override int CompareTo(object obj)
        {
            double otherValue = ((NumberOperand)obj).OperandValue;
            return OperandValue.CompareTo(otherValue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, OperandValue);
        }
        #endregion IOperable Overrides
    }
}
