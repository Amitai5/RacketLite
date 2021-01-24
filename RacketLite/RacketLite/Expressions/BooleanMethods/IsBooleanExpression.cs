using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsBooleanExpression : RacketExpression
    {
        private IsBooleanExpression(List<IRacketObject> args)
            : base("IsBoolean")
        {
            arguments = args;
        }

        public static new IsBooleanExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketAny(str);
            if (arguments?.Count == 1)
            {
                return new IsBooleanExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = arguments[0].Evaluate();
            if (obj is RacketBoolean)
            {
                return new RacketBoolean(true);
            }
            return new RacketBoolean(false);
        }
    }
}
