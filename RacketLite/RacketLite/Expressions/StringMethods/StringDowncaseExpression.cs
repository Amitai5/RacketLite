using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringDowncaseExpression : StringExpression
    {
        private StringDowncaseExpression(List<IRacketObject> args)
            : base("string-downcase")
        {
            parameters = args;
        }

        public static StringDowncaseExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringDowncaseExpression(parameters);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            string strValue = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketString(strValue.ToLower());
        }
    }
}