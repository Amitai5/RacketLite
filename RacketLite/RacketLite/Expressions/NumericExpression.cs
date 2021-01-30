using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public abstract class NumericExpression : RacketExpression
    {
        protected NumericExpression(string name)
            : base(name)
        {
            ReturnType = typeof(RacketNumber);
        }

        public override abstract RacketNumber Evaluate();

        public new static NumericExpression? Parse(string str)
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

            if (ExpressionDefinitions.NumericDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.NumericDefinitions[opCode].Invoke(str);
            }
            return null;
        }
    }
}