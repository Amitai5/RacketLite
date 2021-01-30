using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public abstract class SpecialExpression : RacketExpression
    {
        protected SpecialExpression(string name)
            : base(name)
        {
            ReturnType = typeof(RacketNumber);
        }

        public new static SpecialExpression? Parse(string str)
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

            if (ExpressionDefinitions.SpecialDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.SpecialDefinitions[opCode].Invoke(str);
            }
            return null;
        }
    }
}