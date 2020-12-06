using System.Collections.Generic;
using RacketLite.ValueTypes;
using System.Text;

namespace RacketLite.Expressions
{
    public class SubtractiveExpression : RacketExpression
    {
        private SubtractiveExpression(List<IRacketObject> args)
        {
            arguments = args;
        }

        public static new SubtractiveExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments != null)
            {
                return new SubtractiveExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            bool isExact = true;
            bool isRational = true;
            float retValue = ((RacketNumber)arguments[0].Evaluate()).Value;

            for (int i = 1; i < arguments.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)arguments[i].Evaluate();
                isRational = isRational && currentNumber.IsRational;
                isExact = isExact && currentNumber.IsExact;
                retValue -= currentNumber.Value;

            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append("Subtract").Append('\n');
            ArgumentsToTreeString(stringBuilder, tabIndex + 1);
        }
    }
}
