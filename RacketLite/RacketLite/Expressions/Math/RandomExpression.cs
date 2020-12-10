using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class RandomExpression : RacketExpression
    {
        private static readonly Random random = new Random();

        private RandomExpression(List<IRacketObject> args)
            : base("Random")
        {
            arguments = args;
        }

        public static new RandomExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketInteger(str);
            if (arguments?.Count == 1 && ((RacketInteger)arguments[0].Evaluate()).IsNatural)
            {
                return new RandomExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketInteger(random.Next((int)currentNumber.Value), true);
        }
    }
}
