using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class FloorExpression : RacketExpression
    {
        private FloorExpression(List<IRacketObject> args)
            : base("Floor")
        {
            arguments = args;
        }

        public static new FloorExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new FloorExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketInteger((long)MathF.Floor(currentNumber.Value), true);
        }
    }
}
