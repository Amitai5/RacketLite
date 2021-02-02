using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanNotExpression : BooleanExpression
    {
        private BooleanNotExpression(List<IRacketObject> args)
            : base("Not")
        {
            parameters = args;
        }

        public static BooleanNotExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketBoolean), parameters);
            if (parameters?.Count == 1)
            {
                return new BooleanNotExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketBoolean currentBool = (RacketBoolean)parameters[0].Evaluate();
            return new RacketBoolean(!currentBool.Value);
        }
    }
}
