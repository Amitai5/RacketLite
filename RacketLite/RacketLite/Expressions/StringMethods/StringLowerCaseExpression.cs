using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringLowerCaseExpression : BooleanExpression
    {
        private StringLowerCaseExpression(List<IRacketObject> args)
            : base("StringLowerCase")
        {
            arguments = args;
        }

        public static new StringLowerCaseExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketStrings(str);
            if (arguments?.Count == 1)
            {
                return new StringLowerCaseExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string strValue = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(strValue, "^[a-z]+$"));
        }
    }
}