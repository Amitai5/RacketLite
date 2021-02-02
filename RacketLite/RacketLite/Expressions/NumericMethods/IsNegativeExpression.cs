using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsNegativeExpression : BooleanExpression
    {
        private IsNegativeExpression(List<IRacketObject> args)
            : base("negative?")
        {
            parameters = args;
        }

        public static IsNegativeExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new IsNegativeExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketBoolean(currentNumber.Value < 0);
        }
    }
}
