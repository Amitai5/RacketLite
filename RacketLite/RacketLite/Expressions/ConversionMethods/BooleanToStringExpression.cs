using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanToStringExpression : RacketExpression
    {
        private BooleanToStringExpression(List<IRacketObject> args)
            : base("Boolean->String")
        {
            arguments = args;
        }

        public static new BooleanToStringExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketBoolean.Parse);
            if (arguments?.Count == 1)
            {
                return new BooleanToStringExpression(arguments);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            bool booleanValue = ((RacketBoolean)arguments[0].Evaluate()).Value;
            return new RacketString(booleanValue.ToString().ToLower());
        }
    }
}
