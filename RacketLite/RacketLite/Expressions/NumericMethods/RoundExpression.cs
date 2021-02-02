using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class RoundExpression : NumericExpression
    {
        private RoundExpression(List<IRacketObject> args)
            : base("round")
        {
            parameters = args;
        }

        public static RoundExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new RoundExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Round(currentNumber.Value), currentNumber.IsExact, true);
        }
    }
}
