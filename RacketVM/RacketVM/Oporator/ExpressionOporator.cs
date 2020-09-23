using RacketVM.Exceptions;
using RacketVM.Operands;
using RacketVM.Operands.Variables;
using System;
using System.Collections.Generic;

namespace RacketVM.Oporator
{
    public class ExpressionOporator
    {
        public int OperandMin { get; private set; }
        public int OperandMax { get; private set; }
        public bool KeepVariablePosition { get; private set; }

        public RacketOporator OporatorType { get; set; }
        public string ExpressionText { get; private set; }
        public List<string> UserOperandNames { get; private set; }
        public Dictionary<string, string> LocalizedVarNames { get; private set; }

        public ExpressionOporator(RacketOporator oporator)
        {
            OperandMin = 0;
            OporatorType = oporator;
            UserOperandNames = null;
            OperandMax = int.MaxValue;
            KeepVariablePosition = false;
            LocalizedVarNames = new Dictionary<string, string>();

            switch (OporatorType)
            {
                //Special Oporators
                case RacketOporator.NOP:
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.If:
                    OperandMin = 3;
                    OperandMax = 3;
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.Define:
                    OperandMin = 2;
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.FunctionHeader:
                    KeepVariablePosition = false;
                    UserOperandNames = new List<string>();
                    return;
                case RacketOporator.CreateFunction:
                    OperandMin = 1;
                    KeepVariablePosition = false;
                    return;
                case RacketOporator.UserDefinedFunction:
                    KeepVariablePosition = false;
                    break;


                //Numeric Oporators
                case RacketOporator.Multiply:
                    OperandMin = 2;
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.Divide:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.Add:
                    OperandMin = 2;
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.Subtract:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;

                //Boolean Oporators
                case RacketOporator.Or:
                    OperandMin = 2;
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.Not:
                    OperandMin = 1;
                    OperandMax = 1;
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.And:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.Equal:
                    OperandMin = 2;
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.LessThan:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.GreaterThan:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.LessThanEqualTo:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.GreaterThanEqualTo:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;

                //String Oporators
                case RacketOporator.StringAppend:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.Substring:
                    OperandMin = 2;
                    OperandMax = 3;
                    KeepVariablePosition = true;
                    break;

                case RacketOporator.StringEqual:
                    OperandMin = 2;
                    KeepVariablePosition = false;
                    break;
                case RacketOporator.StringLessThan:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.StringGreaterThan:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.StringLessThanEqualTo:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                case RacketOporator.StringGreaterThanEqualTo:
                    OperandMin = 2;
                    KeepVariablePosition = true;
                    break;
                default:
                    throw new NotImplementedException($"The opCode, \"{OporatorType}\", is not defined as an ExpressionOporator.");
            }
        }

        public void UpdateExpressionText(string expressionText)
        {
            if (string.IsNullOrEmpty(ExpressionText))
            {
                OperandMin = UserOperandNames.Count;
                OperandMax = UserOperandNames.Count;
                ExpressionText = expressionText;
                return;
            }
            throw new Exception("The expression was attempted to be overwritten...");
        }
    }
}