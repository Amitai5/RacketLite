using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringAlphabeticExpression : RacketExpression
    {
        private StringAlphabeticExpression(List<IRacketObject> args)
            : base("StringAlphabetic")
        {
            arguments = args;
        }

        public static new StringAlphabeticExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketString.Parse);
            if (arguments?.Count == 1)
            {
                return new StringAlphabeticExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string value = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(value, "^[A-z]+$"));
        }
    }
}