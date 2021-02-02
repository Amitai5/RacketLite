using System.Collections.Generic;
using RacketLite.ValueTypes;
using System.Text;
using System;

namespace RacketLite.Expressions
{
    public sealed class DefineExpression : SpecialExpression
    {
        public bool FuncDefinition { get; init; }
        public string NewFuncCallName { get; init; }

        private DefineExpression(string newCallName, bool funcDef, List<IRacketObject> args)
            : base("Define")
        {
            parameters = args;
            FuncDefinition = funcDef;
            NewFuncCallName = newCallName;
        }

        public static DefineExpression? Parse(string str)
        {
            bool isFunction = str.StartsWith('(');
            int startIndex = isFunction ? 1 : 0;
            int firstSpace = str.IndexOf(' ');

            string callName = str[startIndex..firstSpace];
            str = str[firstSpace..].Trim();

            if (!isFunction)
            {
                List<IRacketObject>? parameters = RacketParsingHelper.ParseAny(str);
                if(parameters?.Count == 1)
                {
                    return new DefineExpression(callName, isFunction, parameters);
                }
                return null;
            }

            string searchArea = str[0..str.IndexOf(')')];
            string[] varNames = searchArea.Split(' ');
            str = str.Remove(0, searchArea.Length + 1);

            UserDefinedExpression expression = new UserDefinedExpression(callName, str, varNames);
            return new DefineExpression(callName, isFunction, new List<IRacketObject>() { expression });
        }

        public override RacketValueType Evaluate()
        {
            if(ExpressionDefinitions.ContainsPreDefinedKey(NewFuncCallName) || ConstantValueDefinitions.ContainsPreDefinedKey(NewFuncCallName))
            {
                throw new NotImplementedException(); //TODO: SHOW REDUNDANT CALL NAME ERROR
            }

            if(!FuncDefinition)
            {
                RacketValueType value = parameters[0].Evaluate();
                ConstantValueDefinitions.UserDefinedConstants.Add(NewFuncCallName, value);
            }
            else
            {
                UserDefinedExpression expression = (UserDefinedExpression)parameters[0];
                ExpressionDefinitions.UserDefinedExpressions.Add(NewFuncCallName, expression);
            }
            return new RacketVoid();
        }

        #region Override Methods

        public new void ArgumentsToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(NewFuncCallName).Append('\n');

            if (!FuncDefinition)
            {
                base.ArgumentsToTreeString(stringBuilder, tabIndex);
                return;
            }

            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(((UserDefinedExpression)parameters[0]).ExpressionBody).Append('\n');
        }

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
