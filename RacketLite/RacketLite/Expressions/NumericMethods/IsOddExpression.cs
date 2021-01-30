using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsOddExpression : BooleanExpression
    {
        private IsOddExpression(List<IRacketObject> args)
            : base("IsOdd")
        {
            arguments = args;
        }

        public static new IsOddExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.Parse(str, RacketInteger.Parse);
            if (arguments?.Count == 1)
            {
                return new IsOddExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.Value % 2 == 1);
        }
    }
}
