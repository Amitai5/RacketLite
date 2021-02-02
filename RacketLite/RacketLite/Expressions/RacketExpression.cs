using System;
using System.Text;
using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public abstract class RacketExpression : IRacketObject
    {
        public string CallName { get; init; }
        public Type? ReturnType { get; init; }
        protected List<IRacketObject> parameters = new List<IRacketObject>();

        protected RacketExpression(string name, Type? returnType)
        {
            CallName = name;
            ReturnType = returnType;
        }

        public abstract RacketValueType Evaluate();

        #region Base Methods

        public void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(CallName).Append('\n');
            ArgumentsToTreeString(stringBuilder, tabIndex + 1);
        }

        protected void ArgumentsToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                parameters[i].ToTreeString(stringBuilder, tabIndex);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ToTreeString(stringBuilder, 0);
            return stringBuilder.ToString();
        }

        #endregion Base Methods

        public static RacketExpression? Parse(string str, Dictionary<string, IRacketObject>? localVars = null)
        {
            str = str.Trim();
            if (!str.StartsWith('(') || !str.EndsWith(')'))
            {
                return null;
            }

            string opCode;
            (opCode, str) = ParseOpCode(str);
            if (ExpressionDefinitions.SpecialDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.SpecialDefinitions[opCode].Invoke(str); //TODO: FIX THIS
            }

            List<IRacketObject>? parameters;
            if (localVars == null)
            {
                parameters = RacketParsingHelper.ParseAny(str);
            }
            else
            {
                parameters = RacketParsingHelper.ParseAny(str, localVars);
            }

            return Parse(opCode, parameters);
        }

        protected static RacketExpression? Parse(string opCode, List<IRacketObject>? parameters)
        {
            if (ExpressionDefinitions.NumericDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.NumericDefinitions[opCode].Invoke(parameters);
            }
            else if (ExpressionDefinitions.BooleanDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.BooleanDefinitions[opCode].Invoke(parameters);
            }
            else if (ExpressionDefinitions.StringDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.StringDefinitions[opCode].Invoke(parameters);
            }
            else if (ExpressionDefinitions.UserDefinedExpressions.ContainsKey(opCode))
            {
                return UserDefinedExpression.Parse(opCode, parameters);
            }
            throw new UndefinedOperatorException(opCode);
        }

        protected static (string opCode, string str) ParseOpCode(string str)
        {
            string innerString = str[1..^1].Trim();
            string opCode = "";

            if (innerString.Contains(' '))
            {
                int nextParamIndex = innerString.IndexOf(' ');
                opCode = innerString[0..nextParamIndex];
                innerString = innerString.Remove(0, nextParamIndex);
            }
            else
            {
                innerString = "";
            }

            return (opCode, innerString.Trim());
        }
    }
}