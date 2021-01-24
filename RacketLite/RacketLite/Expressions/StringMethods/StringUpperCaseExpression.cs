using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringUpperCaseExpression : RacketExpression
    {
        private StringUpperCaseExpression(List<IRacketObject> args)
            : base("StringUpperCase")
        {
            arguments = args;
        }

        public static new StringUpperCaseExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketString.Parse);
            if (arguments?.Count == 1)
            {
                return new StringUpperCaseExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string strValue = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(strValue, "^[A-Z]+$"));
        }
    }
}