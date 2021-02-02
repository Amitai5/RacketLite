using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class EqualExpression : BooleanExpression
    {
        private EqualExpression(List<IRacketObject> args)
            : base("Equal")
        {
            parameters = args;
        }

        public static EqualExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count > 1)
            {
                return new EqualExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            bool retValue = true;
            RacketNumber firstNumber = (RacketNumber)parameters[0].Evaluate();
            for (int i = 1; i < parameters.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)parameters[i].Evaluate();
                retValue = retValue && firstNumber.IsExact == currentNumber.IsExact;
                retValue = retValue && firstNumber.Value == currentNumber.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
