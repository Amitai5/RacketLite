using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class TangentExpression : RacketExpression
    {
        private TangentExpression(List<IRacketObject> args)
            : base("Cosine")
        {
            arguments = args;
        }

        public static new TangentExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new TangentExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Tan(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
