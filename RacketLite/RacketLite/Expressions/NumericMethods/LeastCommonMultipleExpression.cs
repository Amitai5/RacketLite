using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RacketLite.Expressions
{
    public sealed class LeastCommonMultipleExpression : NumericExpression
    {
        private LeastCommonMultipleExpression(List<IRacketObject> args)
            : base("lcm")
        {
            parameters = args;
        }

        public static LeastCommonMultipleExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketInteger), parameters);
            if (parameters?.Count > 1)
            {
                return new LeastCommonMultipleExpression(parameters);
            }
            throw new ContractViolationException(2, parameters?.Count ?? 0, true);
        }

        public override RacketInteger Evaluate()
        {
            bool isExact = true;
            List<long> lcmValues = new List<long>();

            for (int i = 0; i < parameters.Count; i++)
            {
                RacketInteger racketInteger = (RacketInteger)parameters[i].Evaluate();
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
