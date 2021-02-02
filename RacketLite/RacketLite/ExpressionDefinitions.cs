using RacketLite.Expressions;
using System;
using System.Collections.Generic;

namespace RacketLite
{
    public static class ExpressionDefinitions
    {
        public static Dictionary<string, Func<List<IRacketObject>?, BooleanExpression?>> BooleanDefinitions { get; } = new Dictionary<string, Func<List<IRacketObject>?, BooleanExpression?>>()
        {
            //Standard
            { "and", BooleanAndExpression.Parse },
            { "boolean=?", BooleanEqualExpression.Parse },
            { "boolean?", IsBooleanExpression.Parse },
            { "false?", IsFalseExpression.Parse },
            { "not", BooleanNotExpression.Parse },
            { "or", BooleanOrExpression.Parse },

            //Numeric
            { "=", EqualExpression.Parse },
            { ">", GreaterThanExpression.Parse },
            { ">=", GreaterThanEqualExpression.Parse },
            { "even?", IsEvenExpression.Parse },
            { "exact?", IsExactExpression.Parse },
            { "integer?", IsIntegerExpression.Parse },
            { "natural?", IsNaturalExpression.Parse },
            { "number?", IsNumberExpression.Parse },
            { "negative?", IsNegativeExpression.Parse },
            { "odd?", IsOddExpression.Parse },
            { "positive?", IsPositiveExpression.Parse },
            { "rational?", IsRationalExpression.Parse },
            { "zero?", IsZeroExpression.Parse },
            { "<", LessThanExpression.Parse },
            { "<=", LessThanEqualExpression.Parse },

            //String
            { "string-alphabetic?", StringAlphabeticExpression.Parse},
            { "string-contains", StringContainsExpression.Parse },
            { "string-lower-case?", StringLowerCaseExpression.Parse },
            { "string-numeric?", StringNumericExpression.Parse },
            { "string-upper-case?", StringUpperCaseExpression.Parse },
            { "string-whitespace?", StringWhitespaceExpression.Parse },
        };

        public static Dictionary<string, Func<List<IRacketObject>?, NumericExpression?>> NumericDefinitions { get; } = new Dictionary<string, Func<List<IRacketObject>?, NumericExpression?>>()
        {
            //Standard
            { "abs", AbsoluteValExpression.Parse },
            { "+", AddExpression.Parse },
            { "add1", AddOneExpression.Parse },
            { "acos", ArcCosineExpression.Parse },
            { "asin", ArcSineExpression.Parse },
            { "atan", ArcTangentExpression.Parse },
            { "ceiling", CeilingExpression.Parse },
            { "cos", CosineExpression.Parse },
            { "current-seconds", CurrentSecondsExpression.Parse },
            { "/", DivideExpression.Parse },
            { "expt", ExponentEpression.Parse },
            { "exp", ExponentialExpression.Parse },
            { "floor", FloorExpression.Parse },
            { "gcd", GreatestCommonDivisorExpression.Parse },
            { "cosh", HyperbolicCosineExpression.Parse },
            { "sinh", HyperbolicSineExpression.Parse },
            { "lcm", LeastCommonMultipleExpression.Parse },
            { "max", MaximumExpression.Parse },
            { "min", MinimumExpression.Parse },
            { "modulo", ModuloExpression.Parse },
            { "*", MultiplyExpression.Parse },
            { "log", NaturalLogarithmExpression.Parse },
            { "quotient", QuotientExpression.Parse },
            { "random", RandomExpression.Parse },
            { "remainder", ModuloExpression.Parse },
            { "round", RoundExpression.Parse },
            { "sign", SignExpression.Parse },
            { "sin", SineExpression.Parse },
            { "sqr", SquareExpression.Parse },
            { "sqrt", SquareRootExpression.Parse },
            { "-", SubtractExpression.Parse },
            { "sub1", SubtractOneExpression.Parse },
            { "tan", TangentExpression.Parse },

            //Conversion Methods
            { "boolean->integer", BooleanToIntegerExpression.Parse },
            { "exact->inexact", ExactToInexactExpression.Parse },
            { "inexact->exact", InexactToExactExpression.Parse },

            //String
            { "string-length", StringLengthExpression.Parse },
        };

        public static Dictionary<string, Func<List<IRacketObject>?, StringExpression?>> StringDefinitions { get; } = new Dictionary<string, Func<List<IRacketObject>?, StringExpression?>>()
        {
            //Standard
            { "string-append", StringAppendExpression.Parse},
            { "string-copy", StringCopyExpression.Parse },
            { "string-downcase", StringDowncaseExpression.Parse },
            { "string-upcase", StringUpcaseExpression.Parse },

            //Conversion Methods
            { "number->string", NumberToStringExpression.Parse },
            { "boolean->string", BooleanToStringExpression.Parse },
        };

        public static Dictionary<string, Func<string, SpecialExpression?>> SpecialDefinitions { get; } = new Dictionary<string, Func<string, SpecialExpression?>>()
        {
            { "define", DefineExpression.Parse },
            { "if", IfExpression.Parse },
        };

        public static Dictionary<string, UserDefinedExpression> UserDefinedExpressions = new Dictionary<string, UserDefinedExpression>();

        public static bool ContainsPreDefinedKey(string callName)
        {
            return BooleanDefinitions.ContainsKey(callName) || NumericDefinitions.ContainsKey(callName)
                || StringDefinitions.ContainsKey(callName) || SpecialDefinitions.ContainsKey(callName);
        }

        public static bool ContainsKey(string callName)
        {
            return BooleanDefinitions.ContainsKey(callName) || NumericDefinitions.ContainsKey(callName)
                || StringDefinitions.ContainsKey(callName) || SpecialDefinitions.ContainsKey(callName) || UserDefinedExpressions.ContainsKey(callName);
        }
    }
}