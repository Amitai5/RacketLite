using System.Text;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringAppendExpression : RacketExpression
    {
        private StringAppendExpression(List<IRacketObject> args)
            : base("StringAppend")
        {
            arguments = args;
        }

        public static new StringAppendExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketString.Parse);
            if (arguments?.Count >= 2)
            {
                return new StringAppendExpression(arguments);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arguments.Count; i++)
            {
                sb.Append(((RacketString)arguments[i].Evaluate()).Value);
            }
            return new RacketString(sb.ToString());
        }
    }
}