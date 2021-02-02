using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringContainsExpression : BooleanExpression
    {
        private StringContainsExpression(List<IRacketObject> args)
            : base("string-contains?")
        {
            parameters = args;
        }

        public static StringContainsExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 2)
            {
                return new StringContainsExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            string secondVal = ((RacketString)parameters[1].Evaluate()).Value;
            string firstVal = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketBoolean(secondVal.Contains(firstVal));
        }
    }
}