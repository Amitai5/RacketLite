using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ExponentEpression : NumericExpression
    {
        private ExponentEpression(List<IRacketObject> args)
            : base("Exponent")
        {
            parameters = args;
        }

        public static ExponentEpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 1)
            {
                return new ExponentEpression(parameters);
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
                retValue = MathF.Pow(retValue, currentNumber.Value);
            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }
    }
}
