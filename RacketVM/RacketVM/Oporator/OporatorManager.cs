using System;
using System.Collections.Generic;
using System.Text;

namespace RacketVM.Oporator
{
    public static class OporatorManager
    {
        public static readonly Dictionary<string, RacketOporator> OporatorDefinitions = new Dictionary<string, RacketOporator>()
        {
            { "if", RacketOporator.If },
            { "define", RacketOporator.Define },

            { "*", RacketOporator.Multiply },
            { "/", RacketOporator.Divide },

            { "+", RacketOporator.Add },
            { "-", RacketOporator.Subtract },

            { "or", RacketOporator.Or },
            { "and", RacketOporator.And },
            { "not", RacketOporator.Not },

            { "=", RacketOporator.Equal },
            { "<", RacketOporator.LessThan },
            { ">", RacketOporator.GreaterThan },
            { "<=", RacketOporator.LessThanEqualTo },
            { ">=", RacketOporator.GreaterThanEqualTo },

            { "substring", RacketOporator.Substring },
            { "string-append", RacketOporator.StringAppend },
            { "string-length", RacketOporator.StringLength },

            { "string=?", RacketOporator.StringEqual },
            { "string<?", RacketOporator.StringLessThan },
            { "string>?", RacketOporator.StringGreaterThan },
            { "string<=?", RacketOporator.StringLessThanEqualTo },
            { "string>=?", RacketOporator.StringGreaterThanEqualTo },
        };
    }
}
