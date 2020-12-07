﻿using System.Collections.Generic;
using RacketLite.ValueTypes;
using System.Text;

namespace RacketLite.Expressions
{
    public sealed class AddOneExpression : RacketExpression
    {
        private AddOneExpression(List<IRacketObject> args)
            : base("AddOne")
        {
            arguments = args;
        }

        public static new AddOneExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new AddOneExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return RacketNumber.Parse(currentNumber.Value + 1, currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}