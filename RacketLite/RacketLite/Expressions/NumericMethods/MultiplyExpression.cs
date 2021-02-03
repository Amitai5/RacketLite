using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class MultiplyExpression : NumericExpression
    {
        private MultiplyExpression(List<IRacketObject> args)
            : base("*")
        {
            parameters = args;
        }

        public static MultiplyExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 0)
            {
                return new MultiplyExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0, true);
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            float retValue = currentNumber.Value;
            bool isExact = currentNumber.IsExact;
            bool isRational = currentNumber.IsRational;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentNumber = (RacketNumber)parameters[i].Evaluate();
                isRational = isRational && currentNumber.IsRational;
                isExact = isExact && currentNumber.IsExact;
                retValue *= currentNumber.Value;
            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }
    }
}
