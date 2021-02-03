using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class AddExpression : NumericExpression
    {
        private AddExpression(List<IRacketObject> args)
            : base("+")
        {
            parameters = args;
        }

        public static AddExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 0)
            {
                return new AddExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0, true);
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            bool isRational = currentNumber.IsRational;
            float retValue = currentNumber.Value;
            bool isExact = currentNumber.IsExact;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentNumber = (RacketNumber)parameters[i].Evaluate();
                isRational = isRational && currentNumber.IsRational;
                isExact = isExact && currentNumber.IsExact;
                retValue += currentNumber.Value;
            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }
    }
}
