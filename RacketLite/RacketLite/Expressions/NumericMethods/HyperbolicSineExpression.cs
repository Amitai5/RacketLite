using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class HyperbolicSineExpression : NumericExpression
    {
        private HyperbolicSineExpression(List<IRacketObject> args)
            : base("sinh")
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
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Sinh(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
