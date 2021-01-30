using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class InexactToExactExpression : NumericExpression
    {
        private InexactToExactExpression(List<IRacketObject> args)
            : base("Inexact->Exact")
        {
            arguments = args;
        }

        public static new InexactToExactExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new InexactToExactExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber racketNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketFloat(racketNumber.Value, true, racketNumber.IsRational);
        }
    }
}
