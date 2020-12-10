using System.Collections.Generic;
using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public sealed class IsOddExpression : RacketExpression
    {
        private IsOddExpression(List<IRacketObject> args)
            : base("IsOdd")
        {
            arguments = args;
        }

        public static new IsOddExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new IsOddExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.Value % 2 == 1);
        }
    }
}
