using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringLowerCaseExpression : BooleanExpression
    {
        private StringLowerCaseExpression(List<IRacketObject> args)
            : base("string-lower-case?")
        {
            parameters = args;
        }

        public static StringLowerCaseExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringLowerCaseExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            string strValue = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(strValue, "^[a-z]+$"));
        }
    }
}