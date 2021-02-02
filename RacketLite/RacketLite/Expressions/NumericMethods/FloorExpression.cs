using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class FloorExpression : NumericExpression
    {
        private FloorExpression(List<IRacketObject> args)
            : base("Floor")
        {
            parameters = args;
        }

        public static FloorExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new FloorExpression(parameters);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketInteger((long)MathF.Floor(currentNumber.Value), true);
        }
    }
}
