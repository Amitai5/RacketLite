using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ArcCosineExpression : NumericExpression
    {
        private ArcCosineExpression(List<IRacketObject> args)
            : base("acos")
        {
            parameters = args;
        }

        public static ArcCosineExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new ArcCosineExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Acos(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
