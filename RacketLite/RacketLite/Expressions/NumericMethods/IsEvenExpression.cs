﻿using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsEvenExpression : RacketExpression
    {
        private IsEvenExpression(List<IRacketObject> args)
            : base("IsEven")
        {
            arguments = args;
        }

        public static new IsEvenExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketInteger.Parse);
            if (arguments?.Count == 1)
            {
                return new IsEvenExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketBoolean(currentNumber.Value % 2 == 0);
        }
    }
}