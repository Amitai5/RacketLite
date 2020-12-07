using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

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
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new HyperbolicSineExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Sinh(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
