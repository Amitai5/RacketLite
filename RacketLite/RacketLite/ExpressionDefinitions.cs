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
            { "acos", ArcCosExpression.Parse },
            { "asin", ArcSinExpression.Parse },
            { "atan", ArcTanExpression.Parse },
            { "ceiling", CeilingExpression.Parse },
            { "cos", CosineExpression.Parse },
            { "current-seconds", CurrentSecondsExpression.Parse },
            { "/", DivideExpression.Parse },
            { "=", EqualExpression.Parse },
            { "expt", ExponentEpression.Parse },
            { "exp", ExponentialExpression.Parse },
        };
    }
}
