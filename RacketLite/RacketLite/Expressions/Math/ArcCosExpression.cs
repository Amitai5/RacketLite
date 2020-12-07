using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class ArcCosExpression : RacketExpression
    {
        private ArcCosExpression(List<IRacketObject> args)
            : base("ArcCos")
        {
            arguments = args;
        }

        public static new ArcCosExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new ArcCosExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Acos(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
