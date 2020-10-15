using RacketLite.Exceptions;
using RacketLite.Operands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Oporators
{
    public class RacketOporator
    {
        private int OperandMin { get; }
        private int? OperandMax { get; }
        public RacketOporatorType Type { get; }
        private RacketOperandType[] OperandTypes { get; }

        public RacketOporator(RacketOporatorType oporatorType, int minOperands, int? maxOperands, params RacketOperandType[] operandTypes)
        {
            Type = oporatorType;
            OperandMin = minOperands;
            OperandMax = maxOperands;
            OperandTypes = operandTypes;
        }

        public bool IsValidExpression(OperandQueue operandQueue)
        {
            //If we see NOP, return true
            if(Type == RacketOporatorType.NOP || Type == RacketOporatorType.ReturnExpression)
            {
                return true;
            }

            //Get operand types
            if (Type != RacketOporatorType.Define)
            {
                operandQueue.ValidateOperandQueue();
            }
            RacketOperandType[] operandTypes = operandQueue.OperandTypes;

            //Mask the extra operand for define
            if(Type == RacketOporatorType.Define)
            {
                RacketOperandType[] tempOperandArray = operandTypes;
                operandTypes = new RacketOperandType[operandTypes.Length - 1];
                Array.Copy(tempOperandArray, 1, operandTypes, 0, operandTypes.Length);
            }

            //Check operand bounds
            if (operandTypes.Length < OperandMin)
            {
                throw new ArityMismatchException(Type, OperandMin, operandTypes.Length);
            }
            else if (OperandMax.HasValue && operandTypes.Length > OperandMax.Value)
            {
                throw new ArityMismatchException(Type, OperandMax.Value, operandTypes.Length); //TODO: Write correct function name when user defines it
            }

            //Check for UserDefinedOporator or Define
            if(Type == RacketOporatorType.UserDefinedFunction || Type == RacketOporatorType.Define)
            {
                return true;
            }

            //Ensure oporators conform to given types
            for (int i = 0; i < operandTypes.Length; i++)
            {
                //Check for expressions
                if(operandTypes[i] == RacketOperandType.Expression)
                {
                    continue; //Cannot tell if expressions are not right type at "compile time"
                }

                //Check for unknowns
                if(operandTypes[i] == RacketOperandType.Unknown)
                {
                    string varName = operandQueue.Dequeue(i + 1).OperableValue.ToString();
                    throw new VariableNotFoundException(varName);
                }

                //Check if we run out of operand types
                if (i < OperandTypes.Length)
                {
                    if (operandTypes[i] != OperandTypes[i] && OperandTypes[i] != RacketOperandType.Any)
                    {
                        throw new TypeConversionException(operandTypes[i], OperandTypes[i]);
                    }
                }
                else
                {
                    if (operandTypes[i] != OperandTypes[^1] && OperandTypes[^1] != RacketOperandType.Any)
                    {
                        throw new TypeConversionException(operandTypes[i], OperandTypes[^1]);
                    }
                }
            }
            return true;
        }
    }
}
