using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class CosineExpression : NumericExpression
    {
        private CosineExpression(List<IRacketObject> args)
            : base("Cosine")
        {
            parameters = args;
        }

        public static CosineExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new CosineExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Cos(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
