using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ArcCosineExpression : RacketExpression
    {
        private ArcCosineExpression(List<IRacketObject> args)
            : base("ArcCosine")
        {
            arguments = args;
        }

        public static new ArcCosineExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new ArcCosineExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Acos(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
