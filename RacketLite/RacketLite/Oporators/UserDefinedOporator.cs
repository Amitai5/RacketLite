using RacketLite.Operands;
using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Oporators
{
    public class UserDefinedOporator : RacketOporator
    {
        public string DefinitionString { get; }

        public UserDefinedOporator(string opCode, int minOperands)
            : base(RacketOporatorType.UserDefinedFunction, null, minOperands, minOperands, null)
        {
            DefinitionString = opCode;
        }
    }
}
