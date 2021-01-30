using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsPositiveExpression : BooleanExpression
    {
        private IsPositiveExpression(List<IRacketObject> args)
            : base("IsPositive")
        {
            arguments = args;
        }

        public static new IsPositiveExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new IsPositiveExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.Value > 0);
        }
    }
}
