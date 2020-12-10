using System.Collections.Generic;
using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public sealed class IsNegativeExpression : RacketExpression
    {
        private IsNegativeExpression(List<IRacketObject> args)
            : base("IsNegative")
        {
            arguments = args;
        }

        public static new IsNegativeExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new IsNegativeExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.Value < 0);
        }
    }
}
