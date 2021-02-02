using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class CurrentSecondsExpression : NumericExpression
    {
        private CurrentSecondsExpression(List<IRacketObject> args)
            : base("current-seconds")
        {
            parameters = args;
        }

        public static CurrentSecondsExpression? Parse(List<IRacketObject>? parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return new CurrentSecondsExpression(new List<IRacketObject>());
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            double currentSecondsDouble = Math.Round((DateTime.UtcNow - DateTime.MinValue).TotalSeconds);
            long currentSeconds = Convert.ToInt64(currentSecondsDouble);
            return new RacketInteger(currentSeconds, true);
        }
    }
}
