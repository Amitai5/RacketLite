using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsNaturalExpression : BooleanExpression
    {
        private IsNaturalExpression(List<IRacketObject> args)
            : base("natural?")
        {
            parameters = args;
        }

        public static IsNaturalExpression? Parse(List<IRacketObject>? parameters)
        {
            if (parameters?.Count == 1)
            {
                return new IsNaturalExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = parameters[0].Evaluate();
            if (obj is RacketInteger racketInteger)
            {
                return new RacketBoolean(racketInteger.IsNatural);
            }
            return new RacketBoolean(false);
        }
    }
}
