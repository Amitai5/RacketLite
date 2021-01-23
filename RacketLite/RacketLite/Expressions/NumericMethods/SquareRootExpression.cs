using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SquareRootExpression : RacketExpression
    {
        private SquareRootExpression(List<IRacketObject> args)
            : base("SquareRoot")
        {
            arguments = args;
        }

        public static new SquareRootExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketNumber.Parse);
            if (arguments?.Count == 1)
            {
                return new SquareRootExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Sqrt(currentNumber.Value), currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
