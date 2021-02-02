using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SignExpression : NumericExpression
    {
        private SignExpression(List<IRacketObject> args)
            : base("Sign")
        {
            parameters = args;
        }

        public static SignExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new SignExpression(parameters);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            float value = ((RacketNumber)parameters[0].Evaluate()).Value;
            if (value > 0)
            {
                return new RacketInteger(1, true);
            }
            return new RacketInteger(-1, true);
        }
    }
}
