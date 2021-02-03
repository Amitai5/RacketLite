﻿using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SignExpression : NumericExpression
    {
        private SignExpression(List<IRacketObject> args)
            : base("sign")
        {
            parameters = args;
        }

        public static SignExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new SignExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketInteger Evaluate()
        {
            float value = ((RacketNumber)parameters[0].Evaluate()).Value;
            if (value > 0)
            {
                return new RacketInteger(1, true);
            }
            return new RacketInteger(-1, true);
        }
    }
}
