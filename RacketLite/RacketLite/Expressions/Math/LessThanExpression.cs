using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class LessThanExpression : RacketExpression
    {
        private LessThanExpression(List<IRacketObject> args)
            : base("LessThan")
        {
            arguments = args;
        }

        public static new LessThanExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count > 1)
            {
                return new LessThanExpression(arguments);
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
                retValue = retValue && firstNumber.Value < currentNumber.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
