using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringContainsExpression : RacketExpression
    {
        private StringContainsExpression(List<IRacketObject> args)
            : base("StringContains")
        {
            arguments = args;
        }

        public static new StringContainsExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketString.Parse);
            if (arguments?.Count == 2)
            {
                return new StringContainsExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string secondVal = ((RacketString)arguments[1].Evaluate()).Value;
            string firstVal = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketBoolean(secondVal.Contains(firstVal));
        }
    }
}