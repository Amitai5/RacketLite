using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringUpcaseExpression : StringExpression
    {
        private StringUpcaseExpression(List<IRacketObject> args)
            : base("string-upcase")
        {
            parameters = args;
        }

        public static StringUpcaseExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringUpcaseExpression(parameters);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            string strValue = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketString(strValue.ToUpper());
        }
    }
}