using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class CeilingExpression : NumericExpression
    {
        private CeilingExpression(List<IRacketObject> args)
            : base("Ceiling")
        {
            parameters = args;
        }

        public static CeilingExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new CeilingExpression(parameters);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketInteger((long)MathF.Ceiling(currentNumber.Value), true);
        }
    }
}
