using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public abstract class BooleanExpression : RacketExpression
    {
        protected BooleanExpression(string name)
            : base(name, typeof(RacketBoolean))
        {

        }

        public override abstract RacketBoolean Evaluate();

        public new static BooleanExpression? Parse(string str)
        {
            str = str.Trim();
            if (!str.StartsWith('(') || !str.EndsWith(')'))
            {
                return null;
            }

            string opCode;
            (opCode, str) = parseOpCode(str);

            if (ExpressionDefinitions.BooleanDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.BooleanDefinitions[opCode].Invoke(str);
            }
            return null;
        }
    }
}