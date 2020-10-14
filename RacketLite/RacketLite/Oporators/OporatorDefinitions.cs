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
            { "abs", new RacketOporator(RacketOporatorType.Abs, 1, 1, RacketOperandType.Number) },
            { "+", new RacketOporator(RacketOporatorType.Add, 2, null, RacketOperandType.Number) },
            { "add1", new RacketOporator(RacketOporatorType.AddOne, 1, 1, RacketOperandType.Number) },
            { "acos", new RacketOporator(RacketOporatorType.ArcCosine, 1, 1, RacketOperandType.Number) },
            { "asin", new RacketOporator(RacketOporatorType.ArcSine, 1, 1, RacketOperandType.Number) },
            { "atan", new RacketOporator(RacketOporatorType.ArcTangent, 1, 1, RacketOperandType.Number) },
            { "ceiling", new RacketOporator(RacketOporatorType.Ceiling, 1, 1, RacketOperandType.Number) },
            { "cos", new RacketOporator(RacketOporatorType.Cosine, 1, 1, RacketOperandType.Number) },
            { "/", new RacketOporator(RacketOporatorType.Divide, 2, null, RacketOperandType.Number) },
            { "exp", new RacketOporator(RacketOporatorType.ExponentialPower, 1, 1, RacketOperandType.Number) },
            { "expt", new RacketOporator(RacketOporatorType.Exponent, 2, 2, RacketOperandType.Number) },
            { "floor", new RacketOporator(RacketOporatorType.Floor, 1, 1, RacketOperandType.Number) },
            { "cosh", new RacketOporator(RacketOporatorType.HyperbolicCosine, 1, 1, RacketOperandType.Number) },
            { "sinh", new RacketOporator(RacketOporatorType.HyperbolicSign, 1, 1, RacketOperandType.Number) },
            { "tanh", new RacketOporator(RacketOporatorType.HyperbolicTangent, 1, 1, RacketOperandType.Number) },
            { "log", new RacketOporator(RacketOporatorType.LogBaseE, 1, 1, RacketOperandType.Number) },
            { "modulo", new RacketOporator(RacketOporatorType.Modulo, 2, 2, RacketOperandType.Number) },
            { "*", new RacketOporator(RacketOporatorType.Multiply, 2, null, RacketOperandType.Number) },
            { "random", new RacketOporator(RacketOporatorType.Random, 1, 1, RacketOperandType.Number) },
            { "remainder", new RacketOporator(RacketOporatorType.Remainder, 2, 2, RacketOperandType.Number) },
            { "round", new RacketOporator(RacketOporatorType.Round, 1, 1, RacketOperandType.Number) },
            { "sgn", new RacketOporator(RacketOporatorType.Sign, 1, 1, RacketOperandType.Number) },
            { "sqr", new RacketOporator(RacketOporatorType.Square, 1, 1, RacketOperandType.Number) },
            { "sqrt", new RacketOporator(RacketOporatorType.SquareRoot, 1, 1, RacketOperandType.Number) },
            { "-", new RacketOporator(RacketOporatorType.Subtract, 2, null, RacketOperandType.Number) },
            { "sub1", new RacketOporator(RacketOporatorType.SubtractOne, 1, 1, RacketOperandType.Number) },

            //Numeric Comparisons
            { "=", new RacketOporator(RacketOporatorType.Equal, 2, null, RacketOperandType.Number) },
            { "<", new RacketOporator(RacketOporatorType.LessThan, 2, null, RacketOperandType.Number) },
            { ">", new RacketOporator(RacketOporatorType.GreaterThan, 2, null, RacketOperandType.Number) },
            { "<=", new RacketOporator(RacketOporatorType.LessThanEqualTo, 2, null, RacketOperandType.Number) },
            { ">=", new RacketOporator(RacketOporatorType.GreaterThanEqualTo, 2, null, RacketOperandType.Number) },
            { "zero?", new RacketOporator(RacketOporatorType.CheckZero, 1, 1, RacketOperandType.Number) },

            //Boolean Oporators
            { "and", new RacketOporator(RacketOporatorType.And, 2, null, RacketOperandType.Boolean) },
            { "or", new RacketOporator(RacketOporatorType.Or, 2, null, RacketOperandType.Boolean) },
            { "not", new RacketOporator(RacketOporatorType.Not, 1, 1, RacketOperandType.Boolean) },

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
