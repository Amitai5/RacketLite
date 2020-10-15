using RacketLite.Operands;
using RacketLite.Oporators;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite
{
    public static class StaticsManager
    {
        public static Dictionary<string, DynamicOperand> LocalStack = new Dictionary<string, DynamicOperand>();

        public static readonly Dictionary<string, UserDefinedOporator> userDefinedOporators = new Dictionary<string, UserDefinedOporator>();
        public static readonly Dictionary<string, UserDefinedExpression> UserDefinedExpressions = new Dictionary<string, UserDefinedExpression>();

        public static readonly Dictionary<string, DynamicOperand> VariableMap = new Dictionary<string, DynamicOperand>();
        public static readonly Dictionary<string, DynamicOperand> RacketConstants = new Dictionary<string, DynamicOperand>()
        {
            //Boolean Constants
            { "#t", new BooleanOperand(true) },
            { "#f", new BooleanOperand(false) },
            { "true", new BooleanOperand(true) },
            { "#true", new BooleanOperand(true) },
            { "false", new BooleanOperand(false) },
            { "#false", new BooleanOperand(false) },

            //Numeric Constants
            { "e", new NumberOperand(Math.E) },
            { "pi", new NumberOperand(Math.PI) },
        };
    }
}
