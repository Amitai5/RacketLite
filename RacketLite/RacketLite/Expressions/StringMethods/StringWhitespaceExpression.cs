using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringWhitespaceExpression : BooleanExpression
    {
        private StringWhitespaceExpression(List<IRacketObject> args)
            : base("string-whitespace?")
        {
            parameters = args;
        }

        public static StringWhitespaceExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringWhitespaceExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string value = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(value, "^\\s+$"));
        }
    }
}