using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class ArcSinExpression : RacketExpression
    {
        private ArcSinExpression(List<IRacketObject> args)
            : base("ArcSin")
        {
            arguments = args;
        }

        public static new ArcSinExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new ArcSinExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Asin(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
