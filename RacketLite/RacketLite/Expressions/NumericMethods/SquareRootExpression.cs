﻿using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class SquareRootExpression : NumericExpression
    {
        private SquareRootExpression(List<IRacketObject> args)
            : base("sqrt")
        {
            parameters = args;
        }

        public static SquareRootExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketNumber), parameters);
            if (parameters?.Count == 1)
            {
                return new SquareRootExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketNumber Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)parameters[0].Evaluate();
            return RacketNumber.Parse(MathF.Sqrt(currentNumber.Value), currentNumber.IsExact, currentNumber.IsRational);
        }
    }
}
