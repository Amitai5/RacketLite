using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ArcSineExpression : RacketExpression
    {
        private ArcSineExpression(List<IRacketObject> args)
            : base("ArcSine")
        {
            arguments = args;
        }

        public static new ArcSineExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketNumber.Parse);
            if (arguments?.Count == 1)
            {
                return new ArcSineExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Asin(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
