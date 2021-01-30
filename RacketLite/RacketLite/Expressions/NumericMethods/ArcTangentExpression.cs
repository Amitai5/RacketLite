using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ArcTangentExpression : NumericExpression
    {
        private ArcTangentExpression(List<IRacketObject> args)
            : base("ArcTangent")
        {
            arguments = args;
        }

        public static new ArcTangentExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new ArcTangentExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Atan(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
