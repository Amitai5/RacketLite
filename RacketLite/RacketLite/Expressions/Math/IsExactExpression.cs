using System.Collections.Generic;
using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public sealed class IsExactExpression : RacketExpression
    {
        private IsExactExpression(List<IRacketObject> args)
            : base("IsExact")
        {
            arguments = args;
        }

        public static new IsExactExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new IsExactExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.IsExact);
        }
    }
}
