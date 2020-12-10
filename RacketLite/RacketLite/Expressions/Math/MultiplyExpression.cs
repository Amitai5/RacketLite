using System.Collections.Generic;
using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public sealed class MultiplyExpression : RacketExpression
    {
        private MultiplyExpression(List<IRacketObject> args)
            : base("Multiply")
        {
            arguments = args;
        }

        public static new MultiplyExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count > 0)
            {
                return new MultiplyExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            float retValue = currentNumber.Value;
            bool isExact = currentNumber.IsExact;
            bool isRational = currentNumber.IsRational;

            for (int i = 1; i < arguments.Count; i++)
            {
                currentNumber = (RacketNumber)arguments[i].Evaluate();
                isRational = isRational && currentNumber.IsRational;
                isExact = isExact && currentNumber.IsExact;
                retValue *= currentNumber.Value;

            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }
    }
}
