using System;
using System.Text;
using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public abstract class RacketExpression : IRacketObject
    {
        public string ExpressionName { get; init; }
        public Type? ReturnType { get; init; }
        protected List<IRacketObject> arguments = new List<IRacketObject>();

        protected RacketExpression(string name, Type? returnType)
        {
            ExpressionName = name;
            ReturnType = returnType;
        }

        public abstract RacketValueType Evaluate();

        #region Base Methods

        public void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(ExpressionName).Append('\n');
            ArgumentsToTreeString(stringBuilder, tabIndex + 1);
        }

        protected void ArgumentsToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            for (int i = 0; i < arguments.Count; i++)
            {
                arguments[i].ToTreeString(stringBuilder, tabIndex);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ToTreeString(stringBuilder, 0);
            return stringBuilder.ToString();
        }

        #endregion Base Methods

        public static RacketExpression? Parse(string str)
        {
            str = str.Trim();
            if (!str.StartsWith('(') || !str.EndsWith(')'))
            {
                return null;
            }

            string opCode;
            (opCode, str) = parseOpCode(str);

            if (ExpressionDefinitions.NumericDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.NumericDefinitions[opCode].Invoke(str);
            }
            else if (ExpressionDefinitions.BooleanDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.BooleanDefinitions[opCode].Invoke(str);
            }
            else if (ExpressionDefinitions.StringDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.StringDefinitions[opCode].Invoke(str);
            }
            else if (ExpressionDefinitions.SpecialDefinitions.ContainsKey(opCode))
            {
                return ExpressionDefinitions.SpecialDefinitions[opCode].Invoke(str);
            }
            throw new UndefinedOperatorException(opCode);
        }

        protected static (string opCode, string str) parseOpCode(string str)
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

            return (opCode, innerString);
        }
    }
}