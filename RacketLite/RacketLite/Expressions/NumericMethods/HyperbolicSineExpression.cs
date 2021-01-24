using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class HyperbolicSineExpression : RacketExpression
    {
        private HyperbolicSineExpression(List<IRacketObject> args)
            : base("HyperbolicSine")
        {
            arguments = args;
        }

        public static new HyperbolicSineExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketNumber.Parse);
            if (arguments?.Count == 1)
            {
                return new HyperbolicSineExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Sinh(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
