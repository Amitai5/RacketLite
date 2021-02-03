﻿using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class AbsoluteValExpression : NumericExpression
    {
        private AbsoluteValExpression(List<IRacketObject> args)
            : base("abs")
        {
            parameters = args;
        }

        public static AbsoluteValExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new AbsoluteValExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return new RacketInteger((long)MathF.Abs(currentNumber.Value), currentNumber.IsExact);
        }
    }
}
