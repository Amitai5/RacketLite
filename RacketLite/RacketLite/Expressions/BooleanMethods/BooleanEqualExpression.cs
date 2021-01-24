using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanEqualExpression : RacketExpression
    {
        private BooleanEqualExpression(List<IRacketObject> args)
            : base("BooleanEqual")
        {
            arguments = args;
        }

        public static new BooleanEqualExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketBoolean.Parse);
            if (arguments?.Count > 0)
            {
                return new BooleanEqualExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketBoolean currentBool = (RacketBoolean)arguments[0].Evaluate();
            bool retValue = currentBool.Value;

            for (int i = 1; i < arguments.Count; i++)
            {
                currentBool = (RacketBoolean)arguments[i].Evaluate();
                retValue = retValue == currentBool.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
