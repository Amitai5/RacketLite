using RacketVM.Operands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM
{
    public static class ConstantManager
    {
        public static readonly Dictionary<string, DynamicOperand> ConstantDefinitions = new Dictionary<string, DynamicOperand>()
        {
            //True Boolean
            { "#t", new BooleanOperand(true) },
            { "true", new BooleanOperand(true) },
            { "#true", new BooleanOperand(true) },

            //False Boolean
            { "#f", new BooleanOperand(false) },
            { "false", new BooleanOperand(false) },
            { "#false", new BooleanOperand(false) },

            //Math PI
            { "pi", new NumericOperand(Math.PI) },
            { "PI", new NumericOperand(Math.PI) },
        };
    }
}
