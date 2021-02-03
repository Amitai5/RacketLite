using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringUpperCaseExpression : BooleanExpression
    {
        private StringUpperCaseExpression(List<IRacketObject> args)
            : base("string-upper-case?")
        {
            parameters = args;
        }

        public static StringUpperCaseExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringUpperCaseExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            string strValue = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(strValue, "^[A-Z]+$"));
        }
    }
}