using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public abstract class StringExpression : RacketExpression
    {
        protected StringExpression(string name)
            : base(name, typeof(RacketString))
        {

        }

        public override abstract RacketString Evaluate();

        public new static StringExpression? Parse(string str)
        {
            str = str.Trim();
            if (!str.StartsWith('(') || !str.EndsWith(')'))
            {
                return null;
            }

            string opCode;
            (opCode, str) = parseOpCode(str);

            if (ExpressionDefinitions.StringDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.StringDefinitions[opCode].Invoke(str);
            }
            return null;
        }
    }
}