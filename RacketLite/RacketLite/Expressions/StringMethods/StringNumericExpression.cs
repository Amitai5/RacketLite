using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringNumericExpression : BooleanExpression
    {
        private StringNumericExpression(List<IRacketObject> args)
            : base("StringAlphabetic")
        {
            arguments = args;
        }

        public static new StringNumericExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketStrings(str);
            if (arguments?.Count == 1)
            {
                return new StringNumericExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string value = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(value, "^\\d+$"));
        }
    }
}