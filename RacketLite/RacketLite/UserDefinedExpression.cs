using RacketLite.Exceptions;
using RacketLite.Operands;
using RacketLite.Oporators;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite
{
    public class UserDefinedExpression : RacketExpression
    {
        public UserDefinedExpression(RacketOporator oporator, OperandQueue operands, List<string> localVarNames, Dictionary<string, RacketExpression> innerExpressions)
            : base(null)
        {
            Oporator = oporator;
            Operands = operands;
            LocalVarNames = localVarNames;
            InnerExpressions = innerExpressions;
        }

        public DynamicOperand Evaluate(OperandQueue functionParamQueue)
        {
            //Create var name/value map
            for (int i = 0; i < LocalVarNames.Count; i++)
            {
                DynamicOperand varValue = functionParamQueue.Dequeue();
                if (!StaticsManager.LocalStack.ContainsKey(LocalVarNames[i]))
                {
                    StaticsManager.LocalStack.Add(LocalVarNames[i], varValue);
                }
                else if(varValue.Type != RacketOperandType.Unknown)
                {
                    StaticsManager.LocalStack[LocalVarNames[i]] = varValue;
                }
            }

            //Replace Local Variables
            SetUserDefinedOperands(this, StaticsManager.LocalStack);

            //Run the expression with the new operands
            DynamicOperand result = base.Evaluate();
            return result;
        }

        public new DynamicOperand Evaluate()
        {
            throw new Exception("Not supposed to call Evaluate without specifying params to a UserDefinedExpression.");
        }
    }
}
