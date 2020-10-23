using RacketLite.Parsing;
using System;

namespace RacketLite.Operands
{
    public class NumberOperand : IOperable
    {
        public double OperandValue;
        public bool Inexact { get; private set; }

        public NumberOperand(double value, bool inexact)
            : base(RacketOperandType.Number)
        {
            Inexact = inexact;
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

        public override string ToString()
        {
            string operandValueString = OperandValue.ToString().ToUpper();
            if (Inexact)
            {
                return $"{ParsingRules.InexactNumberPrefix}{operandValueString:0.0}";
            }
            return operandValueString;
        }
        #endregion IOperable Overrides
    }
}
