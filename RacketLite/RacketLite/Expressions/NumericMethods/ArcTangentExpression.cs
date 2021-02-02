using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ArcTangentExpression : NumericExpression
    {
        private ArcTangentExpression(List<IRacketObject> args)
            : base("ArcTangent")
        {
            parameters = args;
        }

        public static ArcTangentExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new ArcTangentExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Atan(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
