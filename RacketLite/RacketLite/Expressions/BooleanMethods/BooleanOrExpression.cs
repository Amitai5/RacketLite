﻿using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class BooleanOrExpression : BooleanExpression
    {
        private BooleanOrExpression(List<IRacketObject> args)
            : base("or")
        {
            parameters = args;
        }

        public static BooleanOrExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketBoolean), parameters);
            if (parameters?.Count > 0)
            {
                return new BooleanOrExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0, true);
        }

        public override RacketBoolean Evaluate()
        {
            RacketBoolean currentBool = (RacketBoolean)parameters[0].Evaluate();
            bool retValue = currentBool.Value;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentBool = (RacketBoolean)parameters[i].Evaluate();
                retValue = retValue || currentBool.Value;
            }
            return new RacketBoolean(retValue);
        }
    }
}
