using System.Text;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IfExpression : RacketExpression
    {
        private IfExpression(List<IRacketObject> args)
            : base("If")
        {
            arguments = args;
        }

        public static new IfExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketAny(str);
            if (arguments?.Count == 3 && arguments[0].Evaluate() is RacketBoolean) //TODO: fix this using parent expressions. Ex: MathExpression, StringExpression, BooleanExpression based on what they return
            {
                return new IfExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            bool questionValue = ((RacketBoolean)arguments[0].Evaluate()).Value;
            if(questionValue)
            {
                return arguments[1].Evaluate();
            }
            return arguments[2].Evaluate();
        }


        #region Override Methods

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.ToTreeString(stringBuilder, 0);
            return stringBuilder.ToString();
        }

        public new void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(ExpressionName).Append('\n');
            this.ArgumentsToTreeString(stringBuilder, tabIndex + 1);
        }

        public new void ArgumentsToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            arguments[0].ToTreeString(stringBuilder, tabIndex);

            stringBuilder.Append('\t', tabIndex - 1);
            stringBuilder.Append("Then").Append('\n');
            arguments[1].ToTreeString(stringBuilder, tabIndex);

            stringBuilder.Append('\t', tabIndex - 1);
            stringBuilder.Append("Else").Append('\n');
            arguments[2].ToTreeString(stringBuilder, tabIndex);
        }

        #endregion Override Methods
    }
}
