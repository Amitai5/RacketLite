using RacketLite.ValueTypes;
using System.Text;
using System;

namespace RacketLite.Expressions
{
    public class AdditiveExpression : RacketExpression
    {
        public AdditiveExpression(string str)
            : base(str)
        {

        }

        public override RacketValueType Evaluate()
        {
            throw new NotImplementedException();
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append("Add").Append('\n');
        }

        public override string GetSignature()
        {
            throw new NotImplementedException();
        }
    }
}
