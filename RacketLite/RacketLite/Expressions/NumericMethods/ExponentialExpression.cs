using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ExponentialExpression : NumericExpression
    {
        private ExponentialExpression(List<IRacketObject> args)
            : base("Exponential")
        {
            parameters = args;
        }

        public static ExponentialExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new ExponentialExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Pow(MathF.E, currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
