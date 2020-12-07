using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class HyperbolicCosineExpression : RacketExpression
    {
        private HyperbolicCosineExpression(List<IRacketObject> args)
            : base("HyperbolicCosine")
        {
            arguments = args;
        }

        public static new HyperbolicCosineExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new HyperbolicCosineExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Cosh(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
