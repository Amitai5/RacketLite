using RacketLite.Expressions;
using System;
using System.Collections.Generic;

namespace RacketLite
{
    public static class ExpressionDefinitions
    {
        public static Dictionary<string, Func<string, RacketExpression?>> MathDefinitions { get; } = new Dictionary<string, Func<string, RacketExpression?>>()
        {
            //Boolean Methods
            { "and", AndExpression.Parse },
            { "boolean=?", BooleanEqualExpression.Parse },
            { "boolean?", IsBooleanExpression.Parse },
            { "false?", IsFalseExpression.Parse },
            { "not", NotExpression.Parse },
            { "or", OrExpression.Parse },


            //Conversion Methods
            { "boolean->integer", BooleanToIntegerExpression.Parse },
            { "boolean->string", BooleanToStringExpression.Parse },
            { "exact->inexact", ExactToInexactExpression.Parse },
            { "inexact->exact", InexactToExactExpression.Parse },
            { "number->string", NumberToStringExpression.Parse },


            //Numeric Methods
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
            { "=", EqualExpression.Parse },
            { "expt", ExponentEpression.Parse },
            { "exp", ExponentialExpression.Parse },
            { "floor", FloorExpression.Parse },
            { ">", GreaterThanExpression.Parse },
            { ">=", GreaterThanEqualExpression.Parse },
            { "gcd", GreatestCommonDivisorExpression.Parse },
            { "cosh", HyperbolicCosineExpression.Parse },
            { "sinh", HyperbolicSineExpression.Parse },
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


            //Special Methods
            { "if", IfExpression.Parse },


            //String Methods
            { "string-alphabetic?", AlphabeticExpression.Parse },
            { "string-whitespace?", WhitespaceExpression.Parse },
            { "string-numeric?", NumericExpression.Parse },
        };
    }
}