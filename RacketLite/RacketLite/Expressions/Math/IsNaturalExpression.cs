using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class IsNaturalExpression : RacketExpression
    {
        private IsNaturalExpression(List<IRacketObject> args)
            : base("IsNatural")
        {
            arguments = args;
        }

        public static new IsNaturalExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumber(str);
            if (arguments?.Count == 1)
            {
                return new IsNaturalExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            bool IsInteger = currentNumber.Value == MathF.Floor(currentNumber.Value);
            return new RacketBoolean(IsInteger && currentNumber.Value > 0);
        }
    }
}
