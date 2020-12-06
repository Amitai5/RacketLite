﻿using System.Collections.Generic;
using RacketLite.ValueTypes;
using System.Text;

namespace RacketLite.Expressions
{
    public class AdditiveExpression : RacketExpression
    {
        private AdditiveExpression(List<IRacketObject> args)
        {
            arguments = args;
        }

        public static new AdditiveExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments != null)
            {
                return new AdditiveExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            float retValue = 0;
            bool isExact = true;
            bool isRational = true;

            for (int i = 0; i < arguments.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)arguments[i].Evaluate();
                isRational = isRational && currentNumber.IsRational;
                isExact = isExact && currentNumber.IsExact;
                retValue += currentNumber.Value;

            }
            return RacketNumber.Parse(retValue, isExact, isRational);
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append("Add").Append('\n');
            ArgumentsToTreeString(stringBuilder, tabIndex + 1);
        }
    }
}
