using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsPositiveExpression : BooleanExpression
    {
        private IsPositiveExpression(List<IRacketObject> args)
            : base("IsPositive")
        {
            parameters = args;
        }

        public static IsPositiveExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new IsPositiveExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketBoolean(currentNumber.Value > 0);
        }
    }
}
