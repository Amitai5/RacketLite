using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite.Expressions
{
    public sealed class CurrentSecondsExpression : RacketExpression
    {
        private CurrentSecondsExpression(List<IRacketObject> args)
            : base("CurrentSeconds")
        {
            arguments = args;
        }

        public static new CurrentSecondsExpression? Parse(string str)
        {
            if (str.Trim()?.Length == 0)
            {
                return new CurrentSecondsExpression(new List<IRacketObject>());
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            double currentSecondsDouble = Math.Round((DateTime.UtcNow - DateTime.MinValue).TotalSeconds);
            long currentSeconds = Convert.ToInt64(currentSecondsDouble);
            return RacketNumber.Parse(currentSeconds, true, true);
        }
    }
}
