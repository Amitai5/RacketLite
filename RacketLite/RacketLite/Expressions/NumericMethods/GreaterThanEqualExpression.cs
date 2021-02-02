using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class GreaterThanEqualExpression : BooleanExpression
    {
        private GreaterThanEqualExpression(List<IRacketObject> args)
            : base("GreaterThanEqual")
        {
            parameters = args;
        }

        public static GreaterThanEqualExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 1)
            {
                return new GreaterThanEqualExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            bool retValue = true;
            RacketNumber firstNumber = (RacketNumber)parameters[0].Evaluate();
            for (int i = 1; i < parameters.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)parameters[i].Evaluate();
                retValue = retValue && firstNumber.Value >= currentNumber.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
