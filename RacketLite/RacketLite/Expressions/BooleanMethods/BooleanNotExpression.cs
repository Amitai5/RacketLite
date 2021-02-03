using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanNotExpression : BooleanExpression
    {
        private BooleanNotExpression(List<IRacketObject> args)
            : base("not")
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
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            RacketBoolean currentBool = (RacketBoolean)parameters[0].Evaluate();
            return new RacketBoolean(!currentBool.Value);
        }
    }
}
