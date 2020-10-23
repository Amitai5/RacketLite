using RacketLite.Operands;
using RacketLite.Oporators;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite
{
    public static class StaticsManager
    {
        public static bool StackContainsInexact
        {
            get => containsInexact;
            set
            {
                if (!containsInexact)
                {
                    containsInexact = true;
                }
            }
        }
        private static bool containsInexact = false;
        public static Dictionary<string, DynamicOperand> LocalStack = new Dictionary<string, DynamicOperand>();
        public static void ResetCurrentStackVars()
        {
            LocalStack.Clear();
            containsInexact = false;
        }

        #region User Defined Expression Methods
        public static void AddUDE(string key, RacketExpression expression)
        {
            userDefinedExpressions.Add(key, expression);
        }

        public static RacketExpression GetUDE(string key)
        {
            return userDefinedExpressions[key].GetCopy();
        }
        #endregion User Defined Expression Methods
        public static readonly Dictionary<string, RacketOporator> UserDefinedOporators = new Dictionary<string, RacketOporator>();
        private static readonly Dictionary<string, RacketExpression> userDefinedExpressions = new Dictionary<string, RacketExpression>();

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
            { "e", new NumberOperand(Math.E, true) },
            { "pi", new NumberOperand(Math.PI, true) },
        };
    }
}
