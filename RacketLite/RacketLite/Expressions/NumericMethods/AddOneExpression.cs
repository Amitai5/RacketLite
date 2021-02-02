using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class AddOneExpression : NumericExpression
    {
        private AddOneExpression(List<IRacketObject> args)
            : base("AddOne")
        {
            parameters = args;
        }

        public static AddOneExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new AddOneExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(currentNumber.Value + 1, currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
