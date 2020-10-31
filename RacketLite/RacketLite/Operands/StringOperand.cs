using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Operands
{
    public class StringOperand : IOperable
    {
        public string StringValue;

        public StringOperand(string value)
            : base(RacketOperandType.String)
        {
            StringValue = value;
        }

        #region IOperable Overrides
        public override bool Equals(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return StringValue.Equals(otherOperand.GetStringValue());
        }

        public override int CompareTo(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return StringValue.CompareTo(otherOperand.GetStringValue());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, StringValue);
        }

        public override string ToString()
        {
            return $"\"{StringValue}\"";
        }
        #endregion IOperable Overrides
    }
}
