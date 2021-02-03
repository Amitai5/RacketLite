using System;
using RacketLite.ValueTypes;
using System.Collections.Generic;
using RacketLite.Exceptions;

namespace RacketLite.Expressions
{
    public sealed class BooleanToIntegerExpression : NumericExpression
    {
        private BooleanToIntegerExpression(List<IRacketObject> args)
            : base("boolean->integer")
        {
            parameters = args;
        }

        public static BooleanToIntegerExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketBoolean), parameters);
            if (parameters?.Count == 1)
            {
                return new BooleanToIntegerExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketInteger Evaluate()
        {
            bool booleanValue = ((RacketBoolean)parameters[0].Evaluate()).Value;
            int integerValue = Convert.ToInt32(booleanValue);
            return new RacketInteger(integerValue, true);
        }
    }
}
