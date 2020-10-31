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
        public RacketOperandType? ReturnType { get; }
        private RacketOperandType[] OperandTypes { get; }

        public RacketOporator(RacketOporatorType oporatorType, RacketOperandType? returnType, int minOperands, int? maxOperands, params RacketOperandType[] operandTypes)
        {
            Type = oporatorType;
            ReturnType = returnType;
            OperandMin = minOperands;
            OperandMax = maxOperands;
            OperandTypes = operandTypes;
        }

        public bool IsValidExpression(OperandQueue operandQueue)
        {
            //If we see NOP, return true
            if (Type == RacketOporatorType.NOP || Type == RacketOporatorType.ReturnExpression)
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
            if (Type == RacketOporatorType.Define)
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

            //Cannot tell if UDF are not right type at "compile time"
            if (Type == RacketOporatorType.UserDefinedFunction || Type == RacketOporatorType.Define)
            {
                return true;
            }

            //Ensure oporators conform to given types
            for (int i = 0; i < operandTypes.Length; i++)
            {
                //Check for expressions
                if (operandTypes[i] == RacketOperandType.Expression)
                {
                    //TODO: FIX THIS
                    continue; //Cannot tell if expressions are not right type at "compile time"
                }

                //Check for unknowns
                if (operandTypes[i] == RacketOperandType.Unknown)
                {
                    string varName = operandQueue.Dequeue(i + 1).ToString();
                    throw new VariableNotFoundException(varName);
                }

                //Parse correct operand type
                int correctOperandType = (int)OperandTypes[^1];
                if (i < OperandTypes.Length)
                {
                    correctOperandType = (int)OperandTypes[i];
                }

                //Check if the expression accepts any
                if (correctOperandType < 0)
                {
                    continue;
                }

                //Check conversion between Number, Integer, and Natural
                if ((RacketOperandType)correctOperandType == RacketOperandType.Number
                    && (RacketOperandType.Number | RacketOperandType.Integer | RacketOperandType.Natural).HasFlag(operandTypes[i]))
                {
                    continue;
                }
                else if ((RacketOperandType)correctOperandType == RacketOperandType.Integer
                    && (RacketOperandType.Integer | RacketOperandType.Natural).HasFlag(operandTypes[i]))
                {
                    continue;
                }

                //Check the operand type
                if ((int)operandTypes[i] != correctOperandType)
                {
                    throw new TypeConversionException(operandTypes[i], OperandTypes[i]);
                }
            }
            return true;
        }

        #region Oporator Signature
        public string GetSignature(int largestKey, int largestOperandList, int largestOperandName)
        {
            //Create signature
            StringBuilder signatureBuilder = new StringBuilder(":");
            signatureBuilder.Append(' ', largestKey);
            signatureBuilder.Append('\t');

            //Add operand string
            string operandString = GetOperandString();
            signatureBuilder.Append(operandString);

            int newOperandPadCount = largestOperandList - operandString.Length;
            signatureBuilder.Append(' ', newOperandPadCount);
            signatureBuilder.Append(" -> ");

            //Check for null return types
            int returnTypeNameLength;
            if (ReturnType.HasValue && ReturnType.Value == RacketOperandType.Any)
            {
                signatureBuilder.Append("Unkown");
                returnTypeNameLength = 6;
            }
            else if (ReturnType.HasValue)
            {
                string returnTypeName = Enum.GetName(typeof(RacketOperandType), ReturnType.Value);
                returnTypeNameLength = returnTypeName.Length;
                signatureBuilder.Append(returnTypeName);
            }
            else
            {
                signatureBuilder.Append("Void");
                returnTypeNameLength = 4;
            }

            //Pad the last part of the string
            int endPadCount = largestOperandName - returnTypeNameLength;
            signatureBuilder.Append(' ', endPadCount);

            //Return the built string
            return signatureBuilder.ToString();
        }

        public string GetOperandString()
        {
            //Create string builder
            StringBuilder stringBuilder = new StringBuilder();

            //Add operand syntax
            if (OperandMin > 0)
            {
                //Get largest value
                int maxOperand = OperandTypes.Length;
                if (OperandMax.HasValue)
                {
                    maxOperand = OperandMax.Value;
                }

                for (int i = 0; i < maxOperand; i++)
                {
                    //Check for universal type
                    string operandName = Enum.GetName(typeof(RacketOperandType), OperandTypes[^1]);
                    if (i < OperandTypes.Length)
                    {
                        operandName = Enum.GetName(typeof(RacketOperandType), OperandTypes[i]);
                    }
                    stringBuilder.Append($"{operandName}");

                    //Check if operand is optional
                    if (OperandMax.HasValue && i > OperandMin - 1)
                    {
                        stringBuilder.Append("?");
                    }
                    stringBuilder.Append(' ');
                }
            }

            //Add the elipses if there is no max
            if (!OperandMax.HasValue)
            {
                stringBuilder.Append("... ");
            }

            //Return built string
            return stringBuilder.ToString();
        }
        #endregion Oporator Signature
    }
}
