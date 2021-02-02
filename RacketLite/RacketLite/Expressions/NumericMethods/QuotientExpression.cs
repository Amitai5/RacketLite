using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class QuotientExpression : NumericExpression
    {
        private QuotientExpression(List<IRacketObject> args)
            : base("Quotient")
        {
            parameters = args;
        }

        public static QuotientExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketInteger), parameters);
            if (parameters?.Count > 0)
            {
                return new QuotientExpression(parameters);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            long retValue = (long)currentNumber.Value;
            bool isExact = currentNumber.IsExact;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentNumber = (RacketNumber)parameters[i].Evaluate();
                isExact = isExact && currentNumber.IsExact;
                retValue /= (long)currentNumber.Value;
            }
            return new RacketInteger(retValue, isExact);
        }
    }
}
