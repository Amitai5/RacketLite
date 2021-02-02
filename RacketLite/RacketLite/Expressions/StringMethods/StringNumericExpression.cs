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
            parameters = args;
        }

        public static StringNumericExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringNumericExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string value = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(value, "^\\d+$"));
        }
    }
}