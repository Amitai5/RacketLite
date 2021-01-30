using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SignExpression : NumericExpression
    {
        private SignExpression(List<IRacketObject> args)
            : base("Sign")
        {
            arguments = args;
        }

        public static new SignExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new SignExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            float value = ((RacketNumber)arguments[0].Evaluate()).Value;
            if (value > 0)
            {
                return new RacketInteger(1, true);
            }
            return new RacketInteger(-1, true);
        }
    }
}
