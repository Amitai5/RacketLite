using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class HyperbolicCosineExpression : NumericExpression
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

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Cosh(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
