using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsFalseExpression : BooleanExpression
    {
        private IsFalseExpression(List<IRacketObject> args)
            : base("IsFalse")
        {
            parameters = args;
        }

        public static IsFalseExpression? Parse(List<IRacketObject>? parameters)
        {
            if (parameters?.Count == 1)
            {
                return new IsFalseExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = parameters[0].Evaluate();
            if (obj is RacketBoolean racketBoolean)
            {
                return new RacketBoolean(!racketBoolean.Value);
            }
            return new RacketBoolean(false);
        }
    }
}
