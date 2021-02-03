using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringLengthExpression : NumericExpression
    {
        private StringLengthExpression(List<IRacketObject> args)
            : base("string-length")
        {
            parameters = args;
        }

        public static StringLengthExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringLengthExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketInteger Evaluate()
        {
            string strValue = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketInteger(strValue.Length, true);
        }
    }
}