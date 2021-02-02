using System.Collections.Generic;
using RacketLite.ValueTypes;
using System;

namespace RacketLite
{
    public static class ConstantValueDefinitions
    {
        public static Dictionary<string, RacketBoolean> BooleanDefinitions { get; } = new Dictionary<string, RacketBoolean>()
        {
            { "#false", new RacketBoolean(false) },
            { "false", new RacketBoolean(false) },
            { "#true", new RacketBoolean(true) },
            { "true", new RacketBoolean(true) },
            { "#f", new RacketBoolean(false) },
            { "#t", new RacketBoolean(true) },
        };

        public static Dictionary<string, RacketNumber> NumericDefinitions { get; } = new Dictionary<string, RacketNumber>()
        {
            { "pi", new RacketFloat(MathF.PI, false, false) }
        };

        public static Dictionary<string, RacketString> StringDefinitions { get; } = new Dictionary<string, RacketString>()
        {

        };

        public static Dictionary<string, RacketValueType> UserDefinedConstants = new Dictionary<string, RacketValueType>();

        public static bool ContainsPreDefinedKey(string callName)
        {
            return BooleanDefinitions.ContainsKey(callName) || NumericDefinitions.ContainsKey(callName) || StringDefinitions.ContainsKey(callName);
        }

        public static bool ContainsKey(string callName)
        {
            return BooleanDefinitions.ContainsKey(callName) || NumericDefinitions.ContainsKey(callName)
                || StringDefinitions.ContainsKey(callName) || UserDefinedConstants.ContainsKey(callName);
        }
    }
}
