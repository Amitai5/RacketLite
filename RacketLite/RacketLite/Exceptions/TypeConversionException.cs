using RacketLite.Operands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Exceptions
{
    public class TypeConversionException : RacketException
    {
        public TypeConversionException(RacketOperandType operandType, RacketOperandType convertToType)
            : base($"Cannot convert {operandType} to {convertToType}.")
        {

        }
    }
}
