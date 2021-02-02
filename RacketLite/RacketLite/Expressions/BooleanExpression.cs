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
    }
}