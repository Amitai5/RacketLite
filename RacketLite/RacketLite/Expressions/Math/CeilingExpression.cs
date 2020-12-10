using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class CeilingExpression : RacketExpression
    {
        private CeilingExpression(List<IRacketObject> args)
            : base("Ceiling")
        {
            arguments = args;
        }

        public static new CeilingExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketNumbers(str);
            if (arguments?.Count == 1)
            {
                return new CeilingExpression(arguments);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            return new RacketInteger((long)MathF.Ceiling(currentNumber.Value), true);
        }
    }
}
