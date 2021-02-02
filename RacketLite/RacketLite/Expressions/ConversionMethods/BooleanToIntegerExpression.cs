using System;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanToIntegerExpression : NumericExpression
    {
        private BooleanToIntegerExpression(List<IRacketObject> args)
            : base("Boolean->Integer")
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
            return null;
        }

        public override RacketInteger Evaluate()
        {
            bool booleanValue = ((RacketBoolean)parameters[0].Evaluate()).Value;
            int integerValue = Convert.ToInt32(booleanValue);
            return new RacketInteger(integerValue, true);
        }
    }
}
