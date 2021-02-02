using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class NumberToStringExpression : StringExpression
    {
        private NumberToStringExpression(List<IRacketObject> args)
            : base("Number->String")
        {
            parameters = args;
        }

        public static NumberToStringExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new NumberToStringExpression(parameters);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            RacketNumber racketNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketString(racketNumber.Value.ToString());
        }
    }
}
