using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ArcSineExpression : NumericExpression
    {
        private ArcSineExpression(List<IRacketObject> args)
            : base("asin")
        {
            parameters = args;
        }

        public static ArcSineExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new ArcSineExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Asin(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
