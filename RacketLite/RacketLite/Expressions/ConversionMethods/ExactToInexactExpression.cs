using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ExactToInexactExpression : NumericExpression
    {
        private ExactToInexactExpression(List<IRacketObject> args)
            : base("exact->inexact")
        {
            parameters = args;
        }

        public static ExactToInexactExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new ExactToInexactExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber racketNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketFloat(racketNumber.Value, false, racketNumber.IsRational);
        }
    }
}
