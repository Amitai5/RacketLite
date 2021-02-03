using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanToStringExpression : StringExpression
    {
        private BooleanToStringExpression(List<IRacketObject> args)
            : base("boolean->string")
        {
            parameters = args;
        }

        public static BooleanToStringExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketBoolean), parameters);
            if (parameters?.Count == 1)
            {
                return new BooleanToStringExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketString Evaluate()
        {
            bool booleanValue = ((RacketBoolean)parameters[0].Evaluate()).Value;
            return new RacketString(booleanValue.ToString().ToLower());
        }
    }
}
