﻿using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Linq;

namespace RacketLite.Expressions
{
    public sealed class GreatestCommonDivisorExpression : NumericExpression
    {
        private GreatestCommonDivisorExpression(List<IRacketObject> args)
            : base("GreatestCommonDivisor")
        {
            arguments = args;
        }

        public static new GreatestCommonDivisorExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketIntegers(str);
            if (arguments?.Count > 1)
            {
                return new GreatestCommonDivisorExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            bool isExact = true;
            List<long> gcdValues = new List<long>();

            for(int i = 0; i < arguments.Count; i++)
            {
                RacketInteger racketInteger = (RacketInteger)arguments[i].Evaluate();
                isExact = isExact && racketInteger.IsExact;
                gcdValues.Add((long)racketInteger.Value);
            }

            long gcd = gcdValues.Aggregate(FindGCD);
            return new RacketInteger(gcd, isExact);
        }

        public static long FindGCD(long a, long b)
        {
            if (b == 0)
            {
                return a;
            }
            return FindGCD(b, a % b);
        }
    }
}
