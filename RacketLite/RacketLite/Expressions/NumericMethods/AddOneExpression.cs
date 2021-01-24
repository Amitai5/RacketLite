using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class AddOneExpression : RacketExpression
    {
        private AddOneExpression(List<IRacketObject> args)
            : base("AddOne")
        {
            arguments = args;
        }

        public static new AddOneExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketNumber.Parse);
            if (arguments?.Count == 1)
            {
                return new AddOneExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(currentNumber.Value + 1, currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
