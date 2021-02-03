using System.Text;
using RacketLite.ValueTypes;
using System.Collections.Generic;
using RacketLite.Exceptions;

namespace RacketLite.Expressions
{
    public sealed class StringAppendExpression : StringExpression
    {
        private StringAppendExpression(List<IRacketObject> args)
            : base("string-append")
        {
            parameters = args;
        }

        public static StringAppendExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count >= 2)
            {
                return new StringAppendExpression(parameters);
            }
            throw new ContractViolationException(2, parameters?.Count ?? 0, true);
        }

        public override RacketString Evaluate()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < parameters.Count; i++)
            {
                sb.Append(((RacketString)parameters[i].Evaluate()).Value);
            }
            return new RacketString(sb.ToString());
        }
    }
}