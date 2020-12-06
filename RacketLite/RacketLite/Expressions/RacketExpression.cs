using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Expressions
{
    public abstract class RacketExpression : IRacketObject
    {
        protected List<RacketValueType> arguments;

        public RacketExpression(string str)
        {

        }

        public abstract string GetSignature();
        public abstract RacketValueType Evaluate();
        public abstract void ToTreeString(StringBuilder stringBuilder, int tabIndex);

        public static RacketExpression Parse(string str)
        {
            if(str.StartsWith("(+"))
            {
                return new AdditiveExpression(str);
            }
            return null;
        }
    }
}