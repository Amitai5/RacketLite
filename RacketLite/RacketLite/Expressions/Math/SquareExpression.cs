using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SquareExpression : RacketExpression
    {
        private SquareExpression(List<IRacketObject> args)
            : base("Square")
        {
            arguments = args;
        }

        public static new SquareExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new SquareExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Pow(currentNumber.Value, 2), currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
