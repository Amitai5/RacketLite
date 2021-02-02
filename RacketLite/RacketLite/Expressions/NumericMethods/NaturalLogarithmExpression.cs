using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class NaturalLogarithmExpression : NumericExpression
    {
        private NaturalLogarithmExpression(List<IRacketObject> args)
            : base("log")
        {
            parameters = args;
        }

        public static NaturalLogarithmExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new NaturalLogarithmExpression(parameters);
            }
            return null;
        }

        public override RacketFloat Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketFloat(MathF.Pow(MathF.E, currentNumber.Value), false, false);
        }
    }
}
