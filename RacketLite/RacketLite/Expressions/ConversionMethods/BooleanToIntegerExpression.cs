using System;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanToIntegerExpression : RacketExpression
    {
        private BooleanToIntegerExpression(List<IRacketObject> args)
            : base("Boolean->Integer")
        {
            arguments = args;
        }

        public static new BooleanToIntegerExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketBoolean.Parse);
            if (arguments?.Count == 1)
            {
                return new BooleanToIntegerExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            bool booleanValue = ((RacketBoolean)arguments[0].Evaluate()).Value;
            int integerValue = Convert.ToInt32(booleanValue);
            return new RacketInteger(integerValue, true);
        }
    }
}
