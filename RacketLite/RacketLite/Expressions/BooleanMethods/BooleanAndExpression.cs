using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanAndExpression : BooleanExpression
    {
        private BooleanAndExpression(List<IRacketObject> args)
            : base("And")
        {
            parameters = args;
        }

        public static BooleanAndExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketBoolean), parameters);
            if (parameters?.Count > 0)
            {
                return new BooleanAndExpression(parameters);
            }
            return null;
        }

        public override RacketBoolean Evaluate()
        {
            RacketBoolean currentBool = (RacketBoolean)parameters[0].Evaluate();
            bool retValue = currentBool.Value;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentBool = (RacketBoolean)parameters[i].Evaluate();
                retValue = retValue && currentBool.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
