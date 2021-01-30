using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsIntegerExpression : BooleanExpression
    {
        private IsIntegerExpression(List<IRacketObject> args)
            : base("IsInteger")
        {
            arguments = args;
        }

        public static new IsIntegerExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseAny(str);
            if (arguments?.Count == 1)
            {
                return new IsIntegerExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = arguments[0].Evaluate();
            if (obj is RacketInteger)
            {
                return new RacketBoolean(true);
            }
            return new RacketBoolean(false);
        }
    }
}
