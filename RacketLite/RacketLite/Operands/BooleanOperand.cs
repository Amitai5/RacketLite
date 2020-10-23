using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Operands
{
    public class BooleanOperand : IOperable
    {
        public bool OperandValue;

        public BooleanOperand(bool value) 
            : base(RacketOperandType.Boolean)
        {
            OperandValue = value;
        }

        #region IOperable Overrides
        public override bool Equals(object obj)
        {
            return OperandValue == ((BooleanOperand)obj).OperandValue;
        }

        public override int CompareTo(object obj)
        {
            bool otherValue = ((BooleanOperand)obj).OperandValue;
            return OperandValue.CompareTo(otherValue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, OperandValue);
        }

        public override string ToString()
        {
            return $"#{OperandValue.ToString().ToLower()}";
        }
        #endregion IOperable Overrides
    }
}
