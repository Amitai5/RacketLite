using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class ArcTanExpression : RacketExpression
    {
        private ArcTanExpression(List<IRacketObject> args)
            : base("ArcTan")
        {
            arguments = args;
        }

        public static new ArcTanExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new ArcTanExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Atan(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
