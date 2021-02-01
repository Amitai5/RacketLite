using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public abstract class NumericExpression : RacketExpression
    {
        protected NumericExpression(string name)
            : base(name, typeof(RacketNumber))
        {

        }

        public override abstract RacketNumber Evaluate();

        public new static NumericExpression? Parse(string str)
        {
            str = str.Trim();
            if (!str.StartsWith('(') || !str.EndsWith(')'))
            {
                return null;
            }

            string opCode;
            (opCode, str) = parseOpCode(str);

            if (ExpressionDefinitions.NumericDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.NumericDefinitions[opCode].Invoke(str);
            }
            return null;
        }
    }
}