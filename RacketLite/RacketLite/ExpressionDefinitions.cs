using System.Collections.Generic;
using RacketLite.Expressions;
using System;

namespace RacketLite
{
    public static class ExpressionDefinitions
    {
        public static Dictionary<string, Func<string, RacketExpression?>> Definitions { get; } = new Dictionary<string, Func<string, RacketExpression?>>()
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

        };
    }
}
