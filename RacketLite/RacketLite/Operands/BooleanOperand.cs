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
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return OperandValue.Equals(otherOperand.GetBooleanValue());
        }

        public override int CompareTo(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return OperandValue.CompareTo(otherOperand.GetBooleanValue());
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
