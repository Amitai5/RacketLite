using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Operands.Variables
{
    public static class VariableManager
    {
        private static readonly Dictionary<string, DynamicOperand> VariableMap = new Dictionary<string, DynamicOperand>();

        public static void AddVar(string varName, DynamicOperand value)
        {
            VariableMap.Add(varName, value);
        }
        
        public static DynamicOperand GetVarValue(string varName)
        {
            return VariableMap[varName];
        }

        public static bool DoesVarExist(string varName)
        {
            return VariableMap.ContainsKey(varName);
        }
    }
}
