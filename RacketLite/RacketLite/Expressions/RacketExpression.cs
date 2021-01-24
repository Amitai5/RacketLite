using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Expressions
{
    public abstract class RacketExpression : IRacketObject
    {
        public string ExpressionName { get; init; }
        protected List<IRacketObject> arguments = new List<IRacketObject>();

        protected RacketExpression(string name)
        {
            ExpressionName = name;
        }

        public abstract RacketValueType Evaluate();

        #region Base Methods

        public void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(ExpressionName).Append('\n');
            ArgumentsToTreeString(stringBuilder, tabIndex + 1);
        }

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

        #endregion Base Methods

        public static RacketExpression? Parse(string str)
        {
            str = str.Trim();
            if (!str.StartsWith('(') || !str.EndsWith(')'))
            {
                return null;
            }

            string opCode = str[1..^1];
            if (str.Contains(' '))
            {
                opCode = str[1..str.IndexOf(' ')];
                str = str[str.IndexOf(' ')..^1].Trim();
            }
            else
            {
                str = "";
            }

            if(ExpressionDefinitions.MathDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.MathDefinitions[opCode].Invoke(str);
            }
            return null;
        }
    }
}