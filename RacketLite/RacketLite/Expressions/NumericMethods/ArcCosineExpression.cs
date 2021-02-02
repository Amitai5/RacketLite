using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ArcCosineExpression : NumericExpression
    {
        private ArcCosineExpression(List<IRacketObject> args)
            : base("ArcCosine")
        {
            parameters = args;
        }

        public static ArcCosineExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new ArcCosineExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Acos(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
