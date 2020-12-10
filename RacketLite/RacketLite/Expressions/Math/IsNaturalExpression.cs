using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsNaturalExpression : RacketExpression
    {
        private IsNaturalExpression(List<IRacketObject> args)
            : base("IsNatural")
        {
            arguments = args;
        }

        public static new IsNaturalExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new IsNaturalExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.IsNatural);
        }
    }
}
