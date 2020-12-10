﻿using System.Collections.Generic;
using RacketLite.ValueTypes;
using System.Text;

namespace RacketLite.Expressions
{
    public sealed class GreaterThanExpression : RacketExpression
    {
        private GreaterThanExpression(List<IRacketObject> args)
            : base("GreaterThan")
        {
            arguments = args;
        }

        public static new GreaterThanExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count > 1)
            {
                return new GreaterThanExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            bool retValue = true;
            RacketNumber firstNumber = (RacketNumber)arguments[0].Evaluate();
            for (int i = 1; i < arguments.Count; i++)
            {
                RacketNumber currentNumber = (RacketNumber)arguments[i].Evaluate();
                retValue = retValue && firstNumber.Value > currentNumber.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
