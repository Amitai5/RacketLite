using System.Collections.Generic;
using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public sealed class LessThanEqualExpression : RacketExpression
    {
        private LessThanEqualExpression(List<IRacketObject> args)
            : base("LessThanEqual")
        {
            arguments = args;
        }

        public static new LessThanEqualExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count > 1)
            {
                return new LessThanEqualExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            bool retValue = true;
            RacketNumber firstNumber = (RacketNumber)arguments[0].Evaluate();
            for (int i = 1; i < arguments.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)arguments[i].Evaluate();
                retValue = retValue && firstNumber.Value <= currentNumber.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
