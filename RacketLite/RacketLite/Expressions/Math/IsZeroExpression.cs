using System.Collections.Generic;
using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public sealed class IsZeroExpression : RacketExpression
    {
        private IsZeroExpression(List<IRacketObject> args)
            : base("IsZero")
        {
            arguments = args;
        }

        public static new IsZeroExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new IsZeroExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.Value == 0);
        }
    }
}
