using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ExponentialExpression : RacketExpression
    {
        private ExponentialExpression(List<IRacketObject> args)
            : base("Exponential")
        {
            arguments = args;
        }

        public static new ExponentialExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new ExponentialExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Pow(MathF.E, currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
