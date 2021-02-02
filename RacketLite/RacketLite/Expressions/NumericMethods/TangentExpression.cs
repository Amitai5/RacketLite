using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class TangentExpression : NumericExpression
    {
        private TangentExpression(List<IRacketObject> args)
            : base("tan")
        {
            parameters = args;
        }

        public static TangentExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new TangentExpression(parameters);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Tan(currentNumber.Value), false, currentNumber.IsRational);
        }
    }
}
