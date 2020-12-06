using System.Collections.Generic;
using RacketLite.ValueTypes;
using System.Text;

namespace RacketLite.Expressions
{
    public abstract class RacketExpression : IRacketObject
    {
        protected List<IRacketObject> arguments = new List<IRacketObject>();

        public abstract RacketValueType Evaluate();
        public abstract void ToTreeString(StringBuilder stringBuilder, int tabIndex);

        protected void ArgumentsToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            for (int i = 0; i < arguments.Count; i++)
            {
                arguments[i].ToTreeString(stringBuilder, tabIndex);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ToTreeString(stringBuilder, 0);
            return stringBuilder.ToString();
        }

        public static RacketExpression? Parse(string str)
        {
            if (!str.StartsWith('(') || !str.EndsWith(')') || !str.Contains(' '))
            {
                return null;
            }

            string opCode = str[1..str.IndexOf(' ')];
            str = str[str.IndexOf(' ')..^1].Trim();

            return opCode switch
            {
                "+" => AdditiveExpression.Parse(str),
                "-" => SubtractiveExpression.Parse(str),
                _ => null
            };
        }
    }
}