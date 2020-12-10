using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class QuotientExpression : RacketExpression
    {
        private QuotientExpression(List<IRacketObject> args)
            : base("Quotient")
        {
            arguments = args;
        }

        public static new QuotientExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count > 0)
            {
                return new QuotientExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            long retValue = (long)currentNumber.Value;
            bool isExact = currentNumber.IsExact;

            for (int i = 1; i < arguments.Count; i++)
            {
                currentNumber = (RacketNumber)arguments[i].Evaluate();
                isExact = isExact && currentNumber.IsExact;
                retValue /= (long)currentNumber.Value;
            }
            return new RacketInteger(retValue, isExact);
        }
    }
}
