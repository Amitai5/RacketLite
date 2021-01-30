using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SineExpression : NumericExpression
    {
        private SineExpression(List<IRacketObject> args)
            : base("Sine")
        {
            arguments = args;
        }

        public static new SineExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new SineExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Sin(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
