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
            arguments = args;
        }

        public static new RandomExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.Parse(str, RacketInteger.Parse);
            if (arguments?.Count == 1 && arguments[0] is RacketExpression)
            {
                return new RandomExpression(arguments);
            }
            else if(arguments?.Count == 1 && ((RacketInteger)arguments[0]).IsNatural)
            {
                return new RandomExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketInteger currentNumber = (RacketInteger)arguments[0].Evaluate();
            return new RacketInteger(random.Next((int)currentNumber.Value), true);
        }
    }
}
