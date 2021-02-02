using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class DivideExpression : NumericExpression
    {
        private DivideExpression(List<IRacketObject> args)
            : base("Divide")
        {
            parameters = args;
        }

        public static DivideExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 0)
            {
                return new DivideExpression(parameters);
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
                retValue /= currentNumber.Value;
            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }
    }
}
