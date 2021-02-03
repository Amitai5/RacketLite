using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsZeroExpression : BooleanExpression
    {
        private IsZeroExpression(List<IRacketObject> args)
            : base("zero?")
        {
            parameters = args;
        }

        public static IsZeroExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new IsZeroExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketBoolean(currentNumber.Value == 0);
        }
    }
}
