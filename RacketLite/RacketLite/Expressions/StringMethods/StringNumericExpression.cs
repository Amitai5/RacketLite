﻿using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RacketLite.Expressions
{
    public sealed class StringNumericExpression : BooleanExpression
    {
        private StringNumericExpression(List<IRacketObject> args)
            : base("string-numeric?")
        {
            parameters = args;
        }

        public static StringNumericExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketString), parameters);
            if (parameters?.Count == 1)
            {
                return new StringNumericExpression(parameters);
            }
            throw new ContractViolationException(1, parameters?.Count ?? 0);
        }

        public override RacketBoolean Evaluate()
        {
            string value = ((RacketString)parameters[0].Evaluate()).Value;
            return new RacketBoolean(Regex.IsMatch(value, "^\\d+$"));
        }
    }
}