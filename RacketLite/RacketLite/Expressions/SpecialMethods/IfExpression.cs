using System.Text;
using RacketLite.ValueTypes;
using System.Collections.Generic;
using RacketLite.Exceptions;

namespace RacketLite.Expressions
{
    public sealed class IfExpression : SpecialExpression
    {
        private IfExpression(List<IRacketObject> args)
            : base("if")
        {
            parameters = args;
        }

        public static IfExpression? Parse(List<IRacketObject>? arguments)
        {
            if (arguments?.Count == 3)
            {
                RacketParsingHelper.ValidateParamType(typeof(RacketBoolean), arguments[0]);
                return new IfExpression(arguments);
            }
            throw new ContractViolationException(3, arguments?.Count ?? 0);
        }

        public override RacketValueType Evaluate()
        {
            bool questionValue = ((RacketBoolean)parameters[0].Evaluate()).Value;
            if (questionValue)
            {
                return parameters[1].Evaluate();
            }
            return parameters[2].Evaluate();
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
            stringBuilder.Append("-> ").Append(CallName).Append('\n');
            this.ArgumentsToTreeString(stringBuilder, tabIndex + 1);
        }

        public new void ArgumentsToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            parameters[0].ToTreeString(stringBuilder, tabIndex);

            stringBuilder.Append('\t', tabIndex - 1);
            stringBuilder.Append("then").Append('\n');
            parameters[1].ToTreeString(stringBuilder, tabIndex);

            stringBuilder.Append('\t', tabIndex - 1);
            stringBuilder.Append("else").Append('\n');
            parameters[2].ToTreeString(stringBuilder, tabIndex);
        }

        #endregion Override Methods
    }
}
