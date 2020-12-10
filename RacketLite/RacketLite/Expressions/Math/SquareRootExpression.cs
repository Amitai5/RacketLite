using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

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
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new SquareRootExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Sqrt(currentNumber.Value), currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
