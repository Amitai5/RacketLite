﻿using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SubtractOneExpression : RacketExpression
    {
        private SubtractOneExpression(List<IRacketObject> args)
            : base("SubtractOne")
        {
            arguments = args;
        }

        public static new SubtractOneExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketNumber.Parse);
            if (arguments?.Count == 1)
            {
                return new SubtractOneExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(currentNumber.Value - 1, currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
