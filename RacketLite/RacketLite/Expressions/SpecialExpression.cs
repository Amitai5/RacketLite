using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public abstract class SpecialExpression : RacketExpression
    {
        protected SpecialExpression(string name)
            : base(name, typeof(RacketValueType))
        {

        }

        public new static SpecialExpression? Parse(string str)
        {
            str = str.Trim();
            if (!str.StartsWith('(') || !str.EndsWith(')'))
            {
                return null;
            }

            string opCode;
            (opCode, str) = parseOpCode(str);

            if (ExpressionDefinitions.SpecialDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.SpecialDefinitions[opCode].Invoke(str);
            }
            return null;
        }
    }
}