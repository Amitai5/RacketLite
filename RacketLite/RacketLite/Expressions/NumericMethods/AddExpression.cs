using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class AddExpression : NumericExpression
    {
        private AddExpression(List<IRacketObject> args)
            : base("Add")
        {
            parameters = args;
        }

        public static AddExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 0)
            {
                return new AddExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            bool isRational = currentNumber.IsRational;
            float retValue = currentNumber.Value;
            bool isExact = currentNumber.IsExact;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentNumber = (RacketNumber)parameters[i].Evaluate();
                isRational = isRational && currentNumber.IsRational;
                isExact = isExact && currentNumber.IsExact;
                retValue += currentNumber.Value;
            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }
    }
}
