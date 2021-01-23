using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class AlphabeticExpression : RacketExpression
    {
        private AlphabeticExpression(List<IRacketObject> args)
            : base("StringAlphabetic")
        {
            arguments = args;
        }

        public static new AlphabeticExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketString.Parse);
            if (arguments?.Count == 1)
            {
                return new AlphabeticExpression(arguments);
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