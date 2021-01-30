using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringUpcaseExpression : StringExpression
    {
        private StringUpcaseExpression(List<IRacketObject> args)
            : base("StringUpcase")
        {
            arguments = args;
        }

        public static new StringUpcaseExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketStrings(str);
            if (arguments?.Count == 1)
            {
                return new StringUpcaseExpression(arguments);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            string strValue = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketString(strValue.ToUpper());
        }
    }
}