using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanEqualExpression : BooleanExpression
    {
        private BooleanEqualExpression(List<IRacketObject> args)
            : base("boolean=?")
        {
            parameters = args;
        }

        public static BooleanEqualExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketBoolean), parameters);
            if (parameters?.Count > 1)
            {
                return new BooleanEqualExpression(parameters);
            }
            throw new ContractViolationException(2, parameters?.Count ?? 0, true);
        }

        public override RacketBoolean Evaluate()
        {
            RacketBoolean currentBool = (RacketBoolean)parameters[0].Evaluate();
            bool retValue = currentBool.Value;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentBool = (RacketBoolean)parameters[i].Evaluate();
                retValue = retValue == currentBool.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
