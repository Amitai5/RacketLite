﻿using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsZeroExpression : RacketExpression
    {
        private IsZeroExpression(List<IRacketObject> args)
            : base("IsZero")
        {
            arguments = args;
        }

        public static new IsZeroExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketNumber.Parse);
            if (arguments?.Count == 1)
            {
                return new IsZeroExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.Value == 0);
        }
    }
}
