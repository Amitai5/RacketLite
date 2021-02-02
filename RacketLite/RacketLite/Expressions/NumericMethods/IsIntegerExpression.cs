using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsIntegerExpression : BooleanExpression
    {
        private IsIntegerExpression(List<IRacketObject> args)
            : base("IsInteger")
        {
            parameters = args;
        }

        public static IsIntegerExpression? Parse(List<IRacketObject>? parameters)
        {
            if (parameters?.Count == 1)
            {
                return new IsIntegerExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = parameters[0].Evaluate();
            if (obj is RacketInteger)
            {
                return new RacketBoolean(true);
            }
            return new RacketBoolean(false);
        }
    }
}
