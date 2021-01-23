using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsNaturalExpression : RacketExpression
    {
        private IsNaturalExpression(List<IRacketObject> args)
            : base("IsNatural")
        {
            arguments = args;
        }

        public static new IsNaturalExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketAny(str);
            if (arguments?.Count == 1)
            {
                return new IsNaturalExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = arguments[0].Evaluate();
            if (obj is RacketInteger racketInteger)
            {
                return new RacketBoolean(racketInteger.IsNatural);
            }
            return new RacketBoolean(false);
        }
    }
}
