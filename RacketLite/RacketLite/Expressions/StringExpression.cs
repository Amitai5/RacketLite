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
    }
}