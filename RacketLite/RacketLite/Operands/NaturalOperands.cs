using RacketLite.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Operands
{
    public class NaturalOperand : IOperable
    {
        public ulong NaturalValue;
        public bool Inexact { get; private set; }

        public NaturalOperand(ulong value, bool inexact)
            : base(RacketOperandType.Natural)
        {
            Inexact = inexact;
            NaturalValue = value;
        }

        #region IOperable Overrides
        public override bool Equals(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return ((double)NaturalValue).Equals(otherOperand.GetNumberValue());
        }

        public override int CompareTo(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return ((double)NaturalValue).CompareTo(otherOperand.GetNumberValue());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, NaturalValue);
        }

        public override string ToString()
        {
            string operandValueString = NaturalValue.ToString().ToUpper();
            if (Inexact)
            {
                return $"{ParsingRules.InexactNumberPrefix}{operandValueString:0.0}";
            }
            return operandValueString;
        }
        #endregion IOperable Overrides
    }
}
