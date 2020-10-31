using RacketLite.Parsing;
using System;

namespace RacketLite.Operands
{
    public class IntegerOperand : IOperable
    {
        public long IntegerValue;
        public bool Inexact { get; private set; }

        public IntegerOperand(long value, bool inexact)
            : base(RacketOperandType.Integer)
        {
            Inexact = inexact;
            IntegerValue = value;
        }

        #region IOperable Overrides
        public override bool Equals(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return ((double)IntegerValue).Equals(otherOperand.GetNumberValue());
        }

        public override int CompareTo(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return ((double)IntegerValue).CompareTo(otherOperand.GetNumberValue());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, IntegerValue);
        }

        public override string ToString()
        {
            string operandValueString = IntegerValue.ToString().ToUpper();
            if (Inexact)
            {
                return $"{ParsingRules.InexactNumberPrefix}{operandValueString:0.0}";
            }
            return operandValueString;
        }
        #endregion IOperable Overrides
    }
}
