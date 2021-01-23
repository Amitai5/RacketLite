using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsFalseExpression : RacketExpression
    {
        private IsFalseExpression(List<IRacketObject> args)
            : base("IsFalse")
        {
            arguments = args;
        }

        public static new IsFalseExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketAny(str);
            if (arguments?.Count == 1)
            {
                return new IsFalseExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = arguments[0].Evaluate();
            if (obj is RacketBoolean racketBoolean)
            {
                return new RacketBoolean(!racketBoolean.Value);
            }
            return new RacketBoolean(false);
        }
    }
}
