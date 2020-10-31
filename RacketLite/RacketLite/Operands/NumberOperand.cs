using RacketLite.Parsing;
using System;

namespace RacketLite.Operands
{
    public class NumberOperand : IOperable
    {
        public double NumberValue;
        public bool Inexact { get; private set; }
        public bool Irrational { get; private set; }

        public NumberOperand(double value, bool inexact)
            : base(RacketOperandType.Number)
        {
            Inexact = inexact;
            NumberValue = value;
            Irrational = Inexact || double.IsNaN(value);
        }

        #region IOperable Overrides
        public override bool Equals(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return ((double)NumberValue).Equals(otherOperand.GetNumberValue());
        }

        public override int CompareTo(object obj)
        {
            DynamicOperand otherOperand = (DynamicOperand)obj;
            return ((double)NumberValue).CompareTo(otherOperand.GetNumberValue());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, NumberValue);
        }

        public override string ToString()
        {
            string operandValueString = NumberValue.ToString().ToUpper();
            if (Inexact)
            {
                return $"{ParsingRules.InexactNumberPrefix}{operandValueString:0.0}";
            }
            return operandValueString;
        }
        #endregion IOperable Overrides
    }
}
