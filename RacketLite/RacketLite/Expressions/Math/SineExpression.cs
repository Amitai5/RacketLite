using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class SineExpression : RacketExpression
    {
        private SineExpression(List<IRacketObject> args)
            : base("Sine")
        {
            arguments = args;
        }

        public static new SineExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new SineExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Sin(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
