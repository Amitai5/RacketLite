using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsRationalExpression : BooleanExpression
    {
        private IsRationalExpression(List<IRacketObject> args)
            : base("IsRational")
        {
            arguments = args;
        }

        public static new IsRationalExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseAny(str);
            if (arguments?.Count == 1)
            {
                return new IsRationalExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = arguments[0].Evaluate();
            if (obj is RacketNumber racketNumber)
            {
                return new RacketBoolean(racketNumber.IsRational);
            }
            return new RacketBoolean(false);
        }
    }
}
