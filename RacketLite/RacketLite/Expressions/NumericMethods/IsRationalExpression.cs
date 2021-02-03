using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsRationalExpression : BooleanExpression
    {
        private IsRationalExpression(List<IRacketObject> args)
            : base("rational?")
        {
            parameters = args;
        }

        public static IsRationalExpression? Parse(List<IRacketObject>? parameters)
        {
            if (parameters?.Count == 1)
            {
                return new IsRationalExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = parameters[0].Evaluate();
            if (obj is RacketNumber racketNumber)
            {
                return new RacketBoolean(racketNumber.IsRational);
            }
            return new RacketBoolean(false);
        }
    }
}
