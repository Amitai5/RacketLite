using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class HyperbolicSineExpression : NumericExpression
    {
        private HyperbolicSineExpression(List<IRacketObject> args)
            : base("HyperbolicSine")
        {
            parameters = args;
        }

        public static HyperbolicSineExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new HyperbolicSineExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Sinh(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
