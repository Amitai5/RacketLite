using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class CosineExpression : RacketExpression
    {
        private CosineExpression(List<IRacketObject> args)
            : base("Cosine")
        {
            arguments = args;
        }

        public static new CosineExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new CosineExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Cos(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
