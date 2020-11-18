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
            { "local", new RacketOporator(RacketOporatorType.Local, RacketOperandType.Any, 2, 2, RacketOperandType.Expression) },
            { "if", new RacketOporator(RacketOporatorType.If, RacketOperandType.Any, 3, 3, RacketOperandType.Expression, RacketOperandType.Any) },

            //Numeric Oporators
            { "abs", new RacketOporator(RacketOporatorType.Abs, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "+", new RacketOporator(RacketOporatorType.Add, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "add1", new RacketOporator(RacketOporatorType.AddOne, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "acos", new RacketOporator(RacketOporatorType.ArcCosine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "asin", new RacketOporator(RacketOporatorType.ArcSine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "atan", new RacketOporator(RacketOporatorType.ArcTangent, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "ceiling", new RacketOporator(RacketOporatorType.Ceiling, RacketOperandType.Integer, 1, 1, RacketOperandType.Number) },
            { "cos", new RacketOporator(RacketOporatorType.Cosine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "current-seconds", new RacketOporator(RacketOporatorType.CurrentSeconds, RacketOperandType.Integer, 0, 0, RacketOperandType.Any) },
            { "/", new RacketOporator(RacketOporatorType.Divide, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "=", new RacketOporator(RacketOporatorType.Equal, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { "expt", new RacketOporator(RacketOporatorType.Exponent, RacketOperandType.Number, 2, 2, RacketOperandType.Number) },
            { "exp", new RacketOporator(RacketOporatorType.Exponential, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "floor", new RacketOporator(RacketOporatorType.Floor, RacketOperandType.Integer, 1, 1, RacketOperandType.Number) },
            { ">", new RacketOporator(RacketOporatorType.GreaterThan, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { ">=", new RacketOporator(RacketOporatorType.GreaterThanOrEqual, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { "gcd", new RacketOporator(RacketOporatorType.GreatestCommonDivisor, RacketOperandType.Integer, 2, null, RacketOperandType.Integer) },
            { "cosh", new RacketOporator(RacketOporatorType.HyperbolicCosine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "sinh", new RacketOporator(RacketOporatorType.HyperbolicSine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "even?", new RacketOporator(RacketOporatorType.IsEven, RacketOperandType.Boolean, 1, 1, RacketOperandType.Integer) },
            { "integer?", new RacketOporator(RacketOporatorType.IsInteger, RacketOperandType.Boolean, 1, 1, RacketOperandType.Any) },
            { "natural?", new RacketOporator(RacketOporatorType.IsNatural, RacketOperandType.Boolean, 1, 1, RacketOperandType.Any) },
            { "negative?", new RacketOporator(RacketOporatorType.IsNegative, RacketOperandType.Boolean, 1, 1, RacketOperandType.Number) },
            { "number?", new RacketOporator(RacketOporatorType.IsNumber, RacketOperandType.Boolean, 1, 1, RacketOperandType.Any) },
            { "odd?", new RacketOporator(RacketOporatorType.IsOdd, RacketOperandType.Boolean, 1, 1, RacketOperandType.Integer) },
            { "rational?", new RacketOporator(RacketOporatorType.IsRational, RacketOperandType.Boolean, 1, 1, RacketOperandType.Any) },
            { "positive?", new RacketOporator(RacketOporatorType.IsPositive, RacketOperandType.Boolean, 1, 1, RacketOperandType.Number) },
            { "zero?", new RacketOporator(RacketOporatorType.IsZero, RacketOperandType.Boolean, 1, 1, RacketOperandType.Number) },
            { "<", new RacketOporator(RacketOporatorType.LessThan, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { "<=", new RacketOporator(RacketOporatorType.LessThanOrEqual, RacketOperandType.Boolean, 2, null, RacketOperandType.Number) },
            { "lcm", new RacketOporator(RacketOporatorType.LeastCommonMultiple, RacketOperandType.Integer, 2, null, RacketOperandType.Integer) },
            { "max", new RacketOporator(RacketOporatorType.Maximum, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "min", new RacketOporator(RacketOporatorType.Minimum, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "modulo", new RacketOporator(RacketOporatorType.Modulo, RacketOperandType.Integer, 2, 2, RacketOperandType.Integer) },
            { "*", new RacketOporator(RacketOporatorType.Multiply, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "log", new RacketOporator(RacketOporatorType.NaturalLog, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "quotient", new RacketOporator(RacketOporatorType.Quotient, RacketOperandType.Integer, 2, 2, RacketOperandType.Integer) },
            { "random", new RacketOporator(RacketOporatorType.Random, RacketOperandType.Natural, 1, 1, RacketOperandType.Natural) },
            { "remainder", new RacketOporator(RacketOporatorType.Remainder, RacketOperandType.Integer, 2, 2, RacketOperandType.Integer) },
            { "round", new RacketOporator(RacketOporatorType.Round, RacketOperandType.Integer, 1, 1, RacketOperandType.Number) },
            { "sgn", new RacketOporator(RacketOporatorType.Sign, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "sin", new RacketOporator(RacketOporatorType.Sine, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "sqr", new RacketOporator(RacketOporatorType.Square, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "sqrt", new RacketOporator(RacketOporatorType.SquareRoot, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "-", new RacketOporator(RacketOporatorType.Subtract, RacketOperandType.Number, 2, null, RacketOperandType.Number) },
            { "sub1", new RacketOporator(RacketOporatorType.SubtractOne, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "tan", new RacketOporator(RacketOporatorType.Tangent, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },

            //Number Conversions
            { "exact->inexact", new RacketOporator(RacketOporatorType.ExactToInexact, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "inexact->exact", new RacketOporator(RacketOporatorType.InexactToExact, RacketOperandType.Number, 1, 1, RacketOperandType.Number) },
            { "number->string", new RacketOporator(RacketOporatorType.NumberToString, RacketOperandType.String, 1, 1, RacketOperandType.Number) },

            //Boolean Oporators
            { "and", new RacketOporator(RacketOporatorType.And, RacketOperandType.Boolean, 2, null, RacketOperandType.Boolean) },
            { "boolean=?", new RacketOporator(RacketOporatorType.BooleanEqual, RacketOperandType.Boolean, 2, 2, RacketOperandType.Boolean) },
            { "boolean?", new RacketOporator(RacketOporatorType.IsBoolean, RacketOperandType.Boolean, 1, 1, RacketOperandType.Any) },
            { "false?", new RacketOporator(RacketOporatorType.IsFalse, RacketOperandType.Boolean, 1, 1, RacketOperandType.Any) },
            { "or", new RacketOporator(RacketOporatorType.Or, RacketOperandType.Boolean, 2, null, RacketOperandType.Boolean) },
            { "not", new RacketOporator(RacketOporatorType.Not, RacketOperandType.Boolean, 1, 1, RacketOperandType.Boolean) },

            //Boolean Conversions
            { "boolean->integer", new RacketOporator(RacketOporatorType.BooleanToInteger, RacketOperandType.Integer, 1, 1, RacketOperandType.Boolean) },
            { "boolean->string", new RacketOporator(RacketOporatorType.BooleanToString, RacketOperandType.String, 1, 1, RacketOperandType.Boolean) },

            //String Oporators
            { "string-length", new RacketOporator(RacketOporatorType.StringLength, RacketOperandType.Integer, 1, 1, RacketOperandType.String) },
            { "string-append", new RacketOporator(RacketOporatorType.StringAppend, RacketOperandType.String, 2, null, RacketOperandType.String) },
            { "substring", new RacketOporator(RacketOporatorType.Substring, RacketOperandType.String, 2, 3, RacketOperandType.String, RacketOperandType.Number, RacketOperandType.Number) },
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
