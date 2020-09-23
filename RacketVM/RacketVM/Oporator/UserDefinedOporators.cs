using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Oporator
{
    public sealed class UserDefinedOporators
    {
        private Dictionary<string, ExpressionOporator> DefinedOporators;

        public static UserDefinedOporators Instance
        {
            get
            {
                return instance;
            }
        }
        private static readonly UserDefinedOporators instance = new UserDefinedOporators();

        private UserDefinedOporators()
        {
            DefinedOporators = new Dictionary<string, ExpressionOporator>();
        }

        public void AddOporator(string opCode, ExpressionOporator expressionOporator)
        {
            DefinedOporators.Add(opCode, expressionOporator);
        }

        public ExpressionOporator GetOporator(string opCode)
        {
            if(!Contains(opCode))
            {
                //TODO: THROW GOOD ERROR
            }
            return DefinedOporators[opCode];
        }

        internal bool Contains(string expressionOpCode)
        {
            return DefinedOporators.ContainsKey(expressionOpCode);
        }
    }
}
