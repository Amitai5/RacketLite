using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringDowncaseExpression : RacketExpression
    {
        private StringDowncaseExpression(List<IRacketObject> args)
            : base("StringDowncase")
        {
            arguments = args;
        }

        public static new StringDowncaseExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketString.Parse);
            if (arguments?.Count == 1)
            {
                return new StringDowncaseExpression(arguments);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            string strValue = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketString(strValue.ToLower());
        }
    }
}