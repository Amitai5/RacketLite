using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringLengthExpression : NumericExpression
    {
        private StringLengthExpression(List<IRacketObject> args)
            : base("StringLength")
        {
            arguments = args;
        }

        public static new StringLengthExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketStrings(str);
            if (arguments?.Count == 1)
            {
                return new StringLengthExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            string strValue = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketInteger(strValue.Length, true);
        }
    }
}