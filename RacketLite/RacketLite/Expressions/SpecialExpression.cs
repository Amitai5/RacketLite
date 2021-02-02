using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public abstract class SpecialExpression : RacketExpression
    {
        protected SpecialExpression(string name)
            : base(name, typeof(RacketValueType))
        {

        }
    }
}