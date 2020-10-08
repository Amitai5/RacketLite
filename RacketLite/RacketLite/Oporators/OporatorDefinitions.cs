using RacketLite.Operands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Oporators
{
    public static class OporatorDefinitions
    {
        public static readonly Dictionary<string, RacketOporator> RacketOporatorMap = new Dictionary<string, RacketOporator>()
        {
            //Special Oporators
            { "define", new RacketOporator(RacketOporatorType.Define, 2, 2, RacketOperandType.Any) },
            { "if", new RacketOporator(RacketOporatorType.If, 3, 3, RacketOperandType.Expression, RacketOperandType.Any) },

            //Numeric Oporators
            { "+", new RacketOporator(RacketOporatorType.Add, 2, null, RacketOperandType.Number) },
            { "/", new RacketOporator(RacketOporatorType.Divide, 2, null, RacketOperandType.Number) },
            { "*", new RacketOporator(RacketOporatorType.Multiply, 2, null, RacketOperandType.Number) },
            { "-", new RacketOporator(RacketOporatorType.Subtract, 2, null, RacketOperandType.Number) },

            //Numeric Comparisons
            { "=", new RacketOporator(RacketOporatorType.Equal, 2, null, RacketOperandType.Number) },
            { "<", new RacketOporator(RacketOporatorType.LessThan, 2, null, RacketOperandType.Number) },
            { ">", new RacketOporator(RacketOporatorType.GreaterThan, 2, null, RacketOperandType.Number) },
            { "<=", new RacketOporator(RacketOporatorType.LessThanEqualTo, 2, null, RacketOperandType.Number) },
            { ">=", new RacketOporator(RacketOporatorType.GreaterThanEqualTo, 2, null, RacketOperandType.Number) },

            //Boolean Oporators
            { "or", new RacketOporator(RacketOporatorType.Or, 2, null, RacketOperandType.Boolean) },
            { "not", new RacketOporator(RacketOporatorType.Not, 1, 1, RacketOperandType.Boolean) },
            { "and", new RacketOporator(RacketOporatorType.And, 2, null, RacketOperandType.Boolean) },

            //String Oporators
            { "string-length", new RacketOporator(RacketOporatorType.StringLength, 1, 1, RacketOperandType.String) },
            { "string-append", new RacketOporator(RacketOporatorType.StringAppend, 2, null, RacketOperandType.String) },
            { "substring", new RacketOporator(RacketOporatorType.Substring, 2, 3, RacketOperandType.String, RacketOperandType.Number, RacketOperandType.Number) },

            //String Comparisons
            { "string=?", new RacketOporator(RacketOporatorType.StringEqual, 2, null, RacketOperandType.String) },
            { "string<?", new RacketOporator(RacketOporatorType.StringLessThan, 2, null, RacketOperandType.String) },
            { "string>?", new RacketOporator(RacketOporatorType.StringGreaterThan, 2, null, RacketOperandType.String) },
            { "string<=?", new RacketOporator(RacketOporatorType.StringLessThanEqualTo, 2, null, RacketOperandType.String) },
            { "string>=?", new RacketOporator(RacketOporatorType.StringGreaterThanEqualTo, 2, null, RacketOperandType.String) },
        };
    }
}
