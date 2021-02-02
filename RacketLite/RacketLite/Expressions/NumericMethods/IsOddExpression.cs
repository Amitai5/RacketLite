using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsOddExpression : BooleanExpression
    {
        private IsOddExpression(List<IRacketObject> args)
            : base("IsOdd")
        {
            parameters = args;
        }

        public static IsOddExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketInteger), parameters);
            if (parameters?.Count == 1)
            {
                return new IsOddExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketBoolean(currentNumber.Value % 2 == 1);
        }
    }
}
