using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class NaturalLogarithmExpression : RacketExpression
    {
        private NaturalLogarithmExpression(List<IRacketObject> args)
            : base("NaturalLog")
        {
            arguments = args;
        }

        public static new NaturalLogarithmExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new NaturalLogarithmExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketFloat(MathF.Pow(MathF.E, currentNumber.Value), false, false);
        }
    }
}
