using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class MinimumExpression : NumericExpression
    {
        private MinimumExpression(List<IRacketObject> args)
            : base("min")
        {
            parameters = args;
        }

        public static MinimumExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 1)
            {
                return new MinimumExpression(parameters);
            }
            throw new ContractViolationException(2, parameters?.Count ?? 0, true);
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber largestNumber = (RacketNumber)parameters[0].Evaluate();
            for (int i = 1; i < parameters.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)parameters[i].Evaluate();
                largestNumber = currentNumber.Value < largestNumber.Value ? currentNumber : largestNumber;
            }
            return largestNumber;
        }
    }
}
