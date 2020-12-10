using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsIntegerExpression : RacketExpression
    {
        private IsIntegerExpression(List<IRacketObject> args)
            : base("IsInteger")
        {
            arguments = args;
        }

        public static new IsIntegerExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new IsIntegerExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.IsInteger);
        }
    }
}
