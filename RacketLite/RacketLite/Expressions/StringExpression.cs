using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public abstract class StringExpression : RacketExpression
    {
        protected StringExpression(string name)
            : base(name)
        {
            ReturnType = typeof(RacketString);
        }

        public override abstract RacketString Evaluate();

        public new static StringExpression? Parse(string str)
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

            if (ExpressionDefinitions.StringDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.StringDefinitions[opCode].Invoke(str);
            }
            return null;
        }
    }
}