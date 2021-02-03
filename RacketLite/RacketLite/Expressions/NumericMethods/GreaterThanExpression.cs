using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class GreaterThanExpression : BooleanExpression
    {
        private GreaterThanExpression(List<IRacketObject> args)
            : base(">")
        {
            parameters = args;
        }

        public static GreaterThanExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 1)
            {
                return new GreaterThanExpression(parameters);
            }
            throw new ContractViolationException(2, parameters?.Count ?? 0, true);
        }

        public override RacketBoolean Evaluate()
        {
            bool retValue = true;
            RacketNumber firstNumber = (RacketNumber)parameters[0].Evaluate();
            for (int i = 1; i < parameters.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)parameters[i].Evaluate();
                retValue = retValue && firstNumber.Value > currentNumber.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
