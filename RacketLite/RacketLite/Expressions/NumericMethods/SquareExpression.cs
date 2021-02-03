using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SquareExpression : NumericExpression
    {
        private SquareExpression(List<IRacketObject> args)
            : base("sqr")
        {
            parameters = args;
        }

        public static SquareExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new SquareExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Pow(currentNumber.Value, 2), currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
