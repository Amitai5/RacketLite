using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsNumberExpression : BooleanExpression
    {
        private IsNumberExpression(List<IRacketObject> args)
            : base("even?")
        {
            parameters = args;
        }

        public static IsNumberExpression? Parse(List<IRacketObject>? parameters)
        {
            if (parameters?.Count == 1)
            {
                return new IsNumberExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = parameters[0].Evaluate();
            if (obj is RacketNumber)
            {
                return new RacketBoolean(true);
            }
            return new RacketBoolean(false);
        }
    }
}
