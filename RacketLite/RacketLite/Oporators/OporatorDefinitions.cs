using RacketLite.Operands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RacketLite.Oporators
{
    public static class OporatorDefinitions
    {
        public static readonly Dictionary<string, RacketOporator> RacketOporatorMap = new Dictionary<string, RacketOporator>()
        {
            //Special Oporators
            { "define", new RacketOporator(RacketOporatorType.Define, null, 2, 2, RacketOperandType.Any) },
            { "if", new RacketOporator(RacketOporatorType.If, RacketOperandType.Any, 3, 3, RacketOperandType.Expression, RacketOperandType.Any) },

            //Numeric Oporators
            { "abs", new RacketOporator(RacketOporatorType.Abs, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "+", new RacketOporator(RacketOporatorType.Add, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "add1", new RacketOporator(RacketOporatorType.AddOne, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "acos", new RacketOporator(RacketOporatorType.ArcCosine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "asin", new RacketOporator(RacketOporatorType.ArcSine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "atan", new RacketOporator(RacketOporatorType.ArcTangent, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "ceiling", new RacketOporator(RacketOporatorType.Ceiling, RacketOperandType.Natural, 1, 1, RacketOperandType.Number) },
            { "cos", new RacketOporator(RacketOporatorType.Cosine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "current-seconds", new RacketOporator(RacketOporatorType.CurrentSeconds, RacketOperandType.Natural, 0, 0, RacketOperandType.Any) },
            { "/", new RacketOporator(RacketOporatorType.Divide, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "exp", new RacketOporator(RacketOporatorType.ExponentialPower, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "expt", new RacketOporator(RacketOporatorType.Exponent, RacketOperandType.Number, 2, 2, RacketOperandType.Number) },
            { "floor", new RacketOporator(RacketOporatorType.Floor, RacketOperandType.Natural, 1, 1, RacketOperandType.Number) },
            { "cosh", new RacketOporator(RacketOporatorType.HyperbolicCosine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "sinh", new RacketOporator(RacketOporatorType.HyperbolicSign, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "tanh", new RacketOporator(RacketOporatorType.HyperbolicTangent, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "log", new RacketOporator(RacketOporatorType.LogBaseE, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "modulo", new RacketOporator(RacketOporatorType.Modulo, RacketOperandType.Natural, 2, 2, RacketOperandType.Natural) },
            { "*", new RacketOporator(RacketOporatorType.Multiply, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "random", new RacketOporator(RacketOporatorType.Random, RacketOperandType.Natural, 1, 1, RacketOperandType.Number) },
            { "remainder", new RacketOporator(RacketOporatorType.Remainder, RacketOperandType.Natural, 2, 2, RacketOperandType.Natural) },
            { "round", new RacketOporator(RacketOporatorType.Round, RacketOperandType.Natural, 1, 1, RacketOperandType.Number) },
            { "sgn", new RacketOporator(RacketOporatorType.Sign, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "sqr", new RacketOporator(RacketOporatorType.Square, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "sqrt", new RacketOporator(RacketOporatorType.SquareRoot, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "-", new RacketOporator(RacketOporatorType.Subtract, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "sub1", new RacketOporator(RacketOporatorType.SubtractOne, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },

            //Numeric Comparisons
            { "=", new RacketOporator(RacketOporatorType.Equal, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { "<", new RacketOporator(RacketOporatorType.LessThan, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { ">", new RacketOporator(RacketOporatorType.GreaterThan, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { "<=", new RacketOporator(RacketOporatorType.LessThanEqualTo, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { ">=", new RacketOporator(RacketOporatorType.GreaterThanEqualTo, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { "even?", new RacketOporator(RacketOporatorType.IsEven, RacketOperandType.Boolean, 1, 1, RacketOperandType.Natural) },
            { "integer?", new RacketOporator(RacketOporatorType.IsInteger, RacketOperandType.Boolean, 1, 1, RacketOperandType.Any) },
            { "negative?", new RacketOporator(RacketOporatorType.IsNegative, RacketOperandType.Boolean, 1, 1, RacketOperandType.Number) },
            { "number?", new RacketOporator(RacketOporatorType.IsNumber, RacketOperandType.Boolean, 1, 1, RacketOperandType.Any) },
            { "odd?", new RacketOporator(RacketOporatorType.IsOdd, RacketOperandType.Boolean, 1, 1, RacketOperandType.Natural) },
            { "positive?", new RacketOporator(RacketOporatorType.IsPositive, RacketOperandType.Boolean, 1, 1, RacketOperandType.Number) },
            { "zero?", new RacketOporator(RacketOporatorType.IsZero, RacketOperandType.Boolean, 1, 1, RacketOperandType.Number) },

            //Boolean Oporators
            { "and", new RacketOporator(RacketOporatorType.And, RacketOperandType.Boolean, 2, null, RacketOperandType.Boolean) },
            { "or", new RacketOporator(RacketOporatorType.Or, RacketOperandType.Boolean, 2, null, RacketOperandType.Boolean) },
            { "not", new RacketOporator(RacketOporatorType.Not, RacketOperandType.Boolean, 1, 1, RacketOperandType.Boolean) },

            //String Oporators
            { "string-length", new RacketOporator(RacketOporatorType.StringLength, RacketOperandType.Natural, 1, 1, RacketOperandType.String) },
            { "string-append", new RacketOporator(RacketOporatorType.StringAppend, RacketOperandType.String, 2, null, RacketOperandType.String) },
            { "substring", new RacketOporator(RacketOporatorType.Substring, RacketOperandType.String, 2, 3, RacketOperandType.String, RacketOperandType.Number, RacketOperandType.Number) },

            //String Comparisons
            { "string=?", new RacketOporator(RacketOporatorType.StringEqual, RacketOperandType.Boolean, 2, null, RacketOperandType.String) },
            { "string<?", new RacketOporator(RacketOporatorType.StringLessThan, RacketOperandType.Boolean, 2, null, RacketOperandType.String) },
            { "string>?", new RacketOporator(RacketOporatorType.StringGreaterThan, RacketOperandType.Boolean, 2, null, RacketOperandType.String) },
            { "string<=?", new RacketOporator(RacketOporatorType.StringLessThanEqualTo, RacketOperandType.Boolean, 2, null, RacketOperandType.String) },
            { "string>=?", new RacketOporator(RacketOporatorType.StringGreaterThanEqualTo, RacketOperandType.Boolean, 2, null, RacketOperandType.String) },
        };

        public static List<string> GetFunctionSignatures()
        {
            //Get largest lengths
            List<string> functionSignatures = new List<string>();
            int largestKey = RacketOporatorMap.Keys.Max(x => x.Length);
            int largestOperandName = Enum.GetNames(typeof(RacketOperandType)).Max(x => x.Length);
            int largestOperandList = RacketOporatorMap.Values.Max(x => x.GetOperandString().Length);

            List<KeyValuePair<string, RacketOporator>> SortedFunctionDefinitions = RacketOporatorMap.OrderBy(x => x.Key).ToList();
            foreach (KeyValuePair<string, RacketOporator> keyValue in SortedFunctionDefinitions)
            {
                string key = keyValue.Key;
                int padCount = largestKey - key.Length; 
                string signatureSuffix = keyValue.Value.GetSignature(padCount, largestOperandList, largestOperandName);
                functionSignatures.Add($"{key}{signatureSuffix}");
            }
            return functionSignatures;
        }
    }
}
