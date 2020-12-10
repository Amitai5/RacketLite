using System.Collections.Generic;
using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public sealed class MaximumExpression : RacketExpression
    {
        private MaximumExpression(List<IRacketObject> args)
            : base("Maximum")
        {
            arguments = args;
        }

        public static new MaximumExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count > 1)
            {
                return new MaximumExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber largestNumber = (RacketNumber)arguments[0].Evaluate();
            for (int i = 1; i < arguments.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)arguments[i].Evaluate();
                largestNumber = currentNumber.Value > largestNumber.Value ? currentNumber : largestNumber;
            }
            return largestNumber;
        }
    }
}
