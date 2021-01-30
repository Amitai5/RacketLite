using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class NaturalLogarithmExpression : NumericExpression
    {
        private NaturalLogarithmExpression(List<IRacketObject> args)
            : base("NaturalLog")
        {
            arguments = args;
        }

        public static new NaturalLogarithmExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new NaturalLogarithmExpression(arguments);
            }
            return null;
        }

        public override RacketFloat Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketFloat(MathF.Pow(MathF.E, currentNumber.Value), false, false);
        }
    }
}
