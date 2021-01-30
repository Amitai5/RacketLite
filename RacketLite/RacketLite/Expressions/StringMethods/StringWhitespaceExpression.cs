using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringWhitespaceExpression : BooleanExpression
    {
        private StringWhitespaceExpression(List<IRacketObject> args)
            : base("StringWhitespace")
        {
            arguments = args;
        }

        public static new StringWhitespaceExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketStrings(str);
            if (arguments?.Count == 1)
            {
                return new StringWhitespaceExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string value = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(value, "^\\s+$"));
        }
    }
}