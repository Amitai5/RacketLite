using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class GreaterThanEqualExpression : BooleanExpression
    {
        private GreaterThanEqualExpression(List<IRacketObject> args)
            : base("GreaterThanEqual")
        {
            arguments = args;
        }

        public static new GreaterThanEqualExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count > 1)
            {
                return new GreaterThanEqualExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            bool retValue = true;
            RacketNumber firstNumber = (RacketNumber)arguments[0].Evaluate();
            for (int i = 1; i < arguments.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)arguments[i].Evaluate();
                retValue = retValue && firstNumber.Value >= currentNumber.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
