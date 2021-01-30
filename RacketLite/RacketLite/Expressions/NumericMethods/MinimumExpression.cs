using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class MinimumExpression : NumericExpression
    {
        private MinimumExpression(List<IRacketObject> args)
            : base("Minimum")
        {
            arguments = args;
        }

        public static new MinimumExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count > 1)
            {
                return new MinimumExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber largestNumber = (RacketNumber)arguments[0].Evaluate();
            for (int i = 1; i < arguments.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)arguments[i].Evaluate();
                largestNumber = currentNumber.Value < largestNumber.Value ? currentNumber : largestNumber;
            }
            return largestNumber;
        }
    }
}
