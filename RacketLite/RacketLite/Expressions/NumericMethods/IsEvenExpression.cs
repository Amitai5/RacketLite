using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsEvenExpression : BooleanExpression
    {
        private IsEvenExpression(List<IRacketObject> args)
            : base("even?")
        {
            parameters = args;
        }

        public static IsEvenExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketInteger), parameters);
            if (parameters?.Count == 1)
            {
                return new IsEvenExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketBoolean(currentNumber.Value % 2 == 0);
        }
    }
}
