using RacketVM.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Operands.Variables
{
    public class Variable : IOperable
    {
        public string Name { get; }

        public Variable(string varName)
            : base(OperandType.Variable)
        {
            if (!VariableManager.DoesVarExist(varName))
            {
                throw new VariableNotFoundException(varName);
            }

            Name = varName;
            Type = VariableManager.GetVarValue(Name).Type;
        }

        public DynamicOperand Evaluate()
        {
            return VariableManager.GetVarValue(Name);
        }

        public override int CompareTo(object obj)
        {
            DynamicOperand otherValue = ((Variable)obj).Evaluate();
            return Evaluate().CompareTo(otherValue);
        }
    }
}
