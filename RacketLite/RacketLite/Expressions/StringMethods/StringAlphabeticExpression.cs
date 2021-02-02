using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringAlphabeticExpression : BooleanExpression
    {
        private StringAlphabeticExpression(List<IRacketObject> args)
            : base("string-alphabetic?")
        {
            parameters = args;
        }

        public static StringAlphabeticExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringAlphabeticExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string value = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(value, "^[A-z]+$"));
        }
    }
}