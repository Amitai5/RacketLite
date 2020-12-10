using System.Collections.Generic;
using RacketLite.Expressions;
using System;

namespace RacketLite
{
    public static class ExpressionDefinitions
    {
        public static Dictionary<string, Func<string, RacketExpression?>> MathDefinitions { get; } = new Dictionary<string, Func<string, RacketExpression?>>()
        {
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
            { "tan", TangentExpression.Parse }
        };
    }
}