using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsNumberExpression : BooleanExpression
    {
        private IsNumberExpression(List<IRacketObject> args)
            : base("IsNumber")
        {
            arguments = args;
        }

        public static new IsNumberExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseAny(str);
            if (arguments?.Count == 1)
            {
                return new IsNumberExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = arguments[0].Evaluate();
            if (obj is RacketNumber)
            {
                return new RacketBoolean(true);
            }
            return new RacketBoolean(false);
        }
    }
}
