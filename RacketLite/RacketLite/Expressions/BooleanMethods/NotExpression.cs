using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class NotExpression : RacketExpression
    {
        private NotExpression(List<IRacketObject> args)
            : base("Not")
        {
            arguments = args;
        }

        public static new NotExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketObjects(str, RacketBoolean.Parse);
            if (arguments?.Count == 1)
            {
                return new NotExpression(arguments);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketBoolean currentBool = (RacketBoolean)arguments[0].Evaluate();
            return new RacketBoolean(!currentBool.Value);
        }
    }
}
