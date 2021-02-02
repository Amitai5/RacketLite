using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SubtractExpression : NumericExpression
    {
        private SubtractExpression(List<IRacketObject> args)
            : base("Subtract")
        {
            parameters = args;
        }

        public static SubtractExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 0)
            {
                return new SubtractExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            float retValue = currentNumber.Value;
            bool isExact = currentNumber.IsExact;
            bool isRational = currentNumber.IsRational;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentNumber = (RacketNumber)parameters[i].Evaluate();
                isRational = isRational && currentNumber.IsRational;
                isExact = isExact && currentNumber.IsExact;
                retValue -= currentNumber.Value;
            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }
    }
}
