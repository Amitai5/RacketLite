using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class CeilingExpression : RacketExpression
    {
        private CeilingExpression(List<IRacketObject> args)
            : base("Ceiling")
        {
            arguments = args;
        }

        public static new CeilingExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new CeilingExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Ceiling(currentNumber.Value), currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
