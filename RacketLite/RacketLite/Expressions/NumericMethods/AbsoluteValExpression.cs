using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class AbsoluteValExpression : RacketExpression
    {
        private AbsoluteValExpression(List<IRacketObject> args)
            : base("Abs")
        {
            arguments = args;
        }

        public static new AbsoluteValExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketNumber.Parse);
            if (arguments?.Count == 1)
            {
                return new AbsoluteValExpression(arguments);
            }
            return null;
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketInteger((long)MathF.Abs(currentNumber.Value), currentNumber.IsExact);
        }
    }
}
