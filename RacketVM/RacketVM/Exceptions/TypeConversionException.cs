using RacketVM.Operands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Exceptions
{
    public class TypeConversionException : RacketVMException
    {
        public TypeConversionException(OperandType operandType, OperandType convertToType)
            : base($"Cannot convert {operandType} to {convertToType}.")
        {

        }
    }
}
