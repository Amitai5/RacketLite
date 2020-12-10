using RacketLite.Expressions;
using RacketLite.ValueTypes;

namespace RacketLite
{
    public static class RacketInterpreter
    {
        public const string RacketLiteVersion = "v3.0 beta";

        public static RacketValueType? ParseLine(string str)
        {
            RacketExpression? expression = RacketExpression.Parse(str);
            if(expression == null)
            {
                return null;
            }

            string expressionTree = expression.ToString();

            try
            {
                return expression.Evaluate();
            }
            catch
            {
                return null; //TODO Include errors
            }
        }
    }
}