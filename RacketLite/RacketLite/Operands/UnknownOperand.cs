using RacketLite.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Operands
{
    public class UnknownOperand : IOperable
    {
        public string OperandValue { get; private set; }

        public UnknownOperand(string value)
            : base(RacketOperandType.Unknown)
        {
            OperandValue = value;
        }

        #region IOperable Overrides
        public override string ToString()
        {
            return OperandValue;
        }

        public override int CompareTo(object obj)
        {
            throw new VariableNotFoundException(OperandValue);
        }
        #endregion IOperable Overrides
    }
}
