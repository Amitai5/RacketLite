using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class EqualExpression : RacketExpression
    {
        private EqualExpression(List<IRacketObject> args)
            : base("Equal")
        {
            arguments = args;
        }

        public static new EqualExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketNumber.Parse);
            if (arguments?.Count > 1)
            {
                return new EqualExpression(arguments);
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
                retValue = retValue && firstNumber.IsExact == currentNumber.IsExact;
                retValue = retValue && firstNumber.Value == currentNumber.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
