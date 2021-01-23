﻿using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class StringCopyExpression : RacketExpression
    {
        private StringCopyExpression(List<IRacketObject> args)
            : base("StringContains")
        {
            arguments = args;
        }

        public static new StringCopyExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketString.Parse);
            if (arguments?.Count == 1)
            {
                return new StringCopyExpression(arguments);
            }
            return null;
        }

        public override RacketString Evaluate()
        {
            string strValue = ((RacketString)arguments[0].Evaluate()).Value;
            return new RacketString(strValue);
        }
    }
}