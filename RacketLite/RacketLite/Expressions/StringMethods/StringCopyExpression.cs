using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringCopyExpression : StringExpression
    {
        private StringCopyExpression(List<IRacketObject> args)
            : base("string-copy")
        {
            parameters = args;
        }

        public static StringCopyExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringCopyExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketString Evaluate()
        {
            string strValue = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketString(strValue);
        }
    }
}