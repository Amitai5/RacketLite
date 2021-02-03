using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class CeilingExpression : NumericExpression
    {
        private CeilingExpression(List<IRacketObject> args)
            : base("ceiling")
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
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketInteger Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketInteger((long)MathF.Ceiling(currentNumber.Value), true);
        }
    }
}
