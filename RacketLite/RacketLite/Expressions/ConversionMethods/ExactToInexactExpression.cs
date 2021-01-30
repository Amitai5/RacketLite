using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ExactToInexactExpression : NumericExpression
    {
        private ExactToInexactExpression(List<IRacketObject> args)
            : base("Exact->Inexact")
        {
            arguments = args;
        }

        public static new ExactToInexactExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new ExactToInexactExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber racketNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketFloat(racketNumber.Value, false, racketNumber.IsRational);
        }
    }
}
