using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringCopyExpression : StringExpression
    {
        private StringCopyExpression(List<IRacketObject> args)
            : base("StringCopy")
        {
            parameters = args;
        }

        public static StringCopyExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringCopyExpression(parameters);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            string strValue = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketString(strValue);
        }
    }
}