using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class RandomExpression : NumericExpression
    {
        private static readonly Random random = new Random();

        private RandomExpression(List<IRacketObject> args)
            : base("Random")
        {
            parameters = args;
        }

        public static RandomExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketInteger), parameters);
            if (parameters?.Count == 1 && parameters[0] is RacketExpression)
            {
                return new RandomExpression(parameters);
            }
            else if(parameters?.Count == 1 && ((RacketInteger)parameters[0]).IsNatural)
            {
                return new RandomExpression(parameters);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketInteger currentNumber = (RacketInteger)parameters[0].Evaluate();
            return new RacketInteger(random.Next((int)currentNumber.Value), true);
        }
    }
}
