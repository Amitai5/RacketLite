using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class HyperbolicCosineExpression : NumericExpression
    {
        private HyperbolicCosineExpression(List<IRacketObject> args)
            : base("cosh")
        {
            parameters = args;
        }

        public static HyperbolicCosineExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new HyperbolicCosineExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Cosh(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
