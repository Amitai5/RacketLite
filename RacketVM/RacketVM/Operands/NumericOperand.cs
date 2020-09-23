using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Operands
{
    public class NumericOperand : IOperable
    {
        public double OperandValue;

        public NumericOperand(double value)
            : base(OperandType.Number)
        {
            OperandValue = value;
        }

        #region Object Overrides

        public override bool Equals(object obj)
        {
            return OperandValue == ((NumericOperand)obj).OperandValue;
        }

        public override int CompareTo(object obj)
        {
            double otherValue = ((NumericOperand)obj).OperandValue;
            return OperandValue.CompareTo(otherValue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, OperandValue);
        }

        #endregion Object Overrides
    }
}
