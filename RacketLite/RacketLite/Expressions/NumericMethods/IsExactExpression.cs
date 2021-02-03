using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsExactExpression : BooleanExpression
    {
        private IsExactExpression(List<IRacketObject> args)
            : base("exact?")
        {
            parameters = args;
        }

        public static IsExactExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new IsExactExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketBoolean(currentNumber.IsExact);
        }
    }
}
