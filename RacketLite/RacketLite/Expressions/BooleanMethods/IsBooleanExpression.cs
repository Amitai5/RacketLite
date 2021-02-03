﻿using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IsBooleanExpression : BooleanExpression
    {
        private IsBooleanExpression(List<IRacketObject> args)
            : base("boolean?")
        {
            parameters = args;
        }

        public static IsBooleanExpression? Parse(List<IRacketObject>? parameters)
        {
            if (parameters?.Count == 1)
            {
                return new IsBooleanExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            IRacketObject obj = parameters[0].Evaluate();
            if (obj is RacketBoolean)
            {
                return new RacketBoolean(true);
            }
            return new RacketBoolean(false);
        }
    }
}
