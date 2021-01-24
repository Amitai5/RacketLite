using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RacketLite.Expressions
{
    public sealed class LeastCommonMultipleExpression : RacketExpression
    {
        private LeastCommonMultipleExpression(List<IRacketObject> args)
            : base("LeastCommonMultiple")
        {
            arguments = args;
        }

        public static new LeastCommonMultipleExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketInteger.Parse);
            if (arguments?.Count > 1)
            {
                return new LeastCommonMultipleExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            bool isExact = true;
            List<long> lcmValues = new List<long>();

            for (int i = 0; i < arguments.Count; i++)
            {
                RacketInteger racketInteger = (RacketInteger)arguments[i].Evaluate();
                isExact = isExact && racketInteger.IsExact;
                lcmValues.Add((long)racketInteger.Value);
            }

            long lcm = lcmValues.Aggregate(FindLCM);
            return new RacketInteger(lcm, isExact);
        }

        public static long FindLCM(long a, long b)
        {
            return (long)(MathF.Abs(a * b) / GreatestCommonDivisorExpression.FindGCD(a, b));
        }
    }
}
