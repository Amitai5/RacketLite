using System;
using System.Text;
using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class UserDefinedExpression : RacketExpression
    {
        public int ParamCount { get; init; }
        public string ExpressionBody { get; init; }
        private string[] VariableNames { get; init; }

        public UserDefinedExpression(string callName, string expressionBody, string[] variableNames)
            : base(callName, typeof(RacketValueType))
        {
            VariableNames = variableNames;
            ParamCount = variableNames.Length;
            ExpressionBody = expressionBody.Trim();
        }

        public static new RacketExpression? Parse(string opCode, List<IRacketObject>? parameters)
        {
            if (!ExpressionDefinitions.UserDefinedExpressions.ContainsKey(opCode))
            {
                return null;
            }

            UserDefinedExpression expression = ExpressionDefinitions.UserDefinedExpressions[opCode];
            if (parameters == null && expression.ParamCount == 0)
            {
                return expression;
            }
            else if (parameters?.Count == expression.ParamCount)
            {
                return expression.SetParamValues(parameters);
            }
            throw new ContractViolationException(expression.ParamCount, (parameters?.Count) ?? 0);
        }

        public RacketExpression? SetParamValues(List<IRacketObject> paramValues)
        {
            Dictionary<string, IRacketObject> localVars = new Dictionary<string, IRacketObject>();
            for(int i = 0; i < VariableNames.Length; i++)
            {
                localVars.Add(VariableNames[i], paramValues[i]);
            }

            (string opCode, string exprBody) = ParseOpCode(ExpressionBody);
            List<IRacketObject>? innerFuncParams = RacketParsingHelper.ParseAny(exprBody, localVars);
            return RacketExpression.Parse(opCode, innerFuncParams);
        }

        public override RacketValueType Evaluate()
        {
            throw new ArgumentNullException($"The parameters to the UserDefinedExpression, {CallName}, were not set!");
        }

        #region Override Methods

        public new void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(CallName).Append('\n');
            ArgumentsToTreeString(stringBuilder, tabIndex + 1);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ToTreeString(stringBuilder, 0);
            return stringBuilder.ToString();
        }

        #endregion Override Methods
    }
}
