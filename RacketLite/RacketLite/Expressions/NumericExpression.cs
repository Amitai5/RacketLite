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
    }
}