using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class InexactToExactExpression : NumericExpression
    {
        private InexactToExactExpression(List<IRacketObject> args)
            : base("inexact->exact")
        {
            parameters = args;
        }

        public static InexactToExactExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new InexactToExactExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber racketNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketFloat(racketNumber.Value, true, racketNumber.IsRational);
        }
    }
}
