using System;
using System.Collections.Generic;
using System.Text;

namespace RacketLite.Parsing
{
    public static class ParsingRules
    {
        public const string ExpressionPerface = "##";
        public const string ReservedLocalPreface = "#";
        public static readonly char[] InvalidCharacters = { '"', ',', '`', '(', ')', '[', ']', '{', '}', '|', ';', '#' };
    }
}
