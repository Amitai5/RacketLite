﻿using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class RoundExpression : RacketExpression
    {
        private RoundExpression(List<IRacketObject> args)
            : base("Round")
        {
            arguments = args;
        }

        public static new RoundExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new RoundExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(MathF.Round(currentNumber.Value), currentNumber.IsExact, true);
        }
    }
}