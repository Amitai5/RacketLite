using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class NumberToStringExpression : StringExpression
    {
        private NumberToStringExpression(List<IRacketObject> args)
            : base("Number->String")
        {
            arguments = args;
        }

        public static new NumberToStringExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.Parse(str, RacketNumber.Parse);
            if (arguments?.Count == 1)
            {
                return new NumberToStringExpression(arguments);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            RacketNumber racketNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketString(racketNumber.Value.ToString());
        }
    }
}
