using RacketLite.Expressions;
using RacketLite.ValueTypes;
using System;
using System.Collections.Generic;

namespace RacketLite
{
    public static class RacketParsingHelper
    {
        public const string InexactNumberPrefix = "#i";

        public static List<IRacketObject>? ParseRacketAny(string str)
        {
            string[] args = str.Split(" ");
            List<IRacketObject> arguments = new List<IRacketObject>();

            string predicate = "";
            for (int i = 0; i < args.Length; i++)
            {
                string currentToken = predicate + args[i];
                IRacketObject? newRacketObject = RacketBoolean.Parse(currentToken)
                    ?? RacketNumber.Parse(currentToken) ?? RacketString.Parse(currentToken) ?? (IRacketObject?)RacketExpression.Parse(currentToken);

                if (newRacketObject != null)
                {
                    arguments.Add(newRacketObject);
                    predicate = "";
                    continue;
                }
                predicate += $"{args[i]} ";
            }

            if (predicate != "")
            {
                return null;
            }
            return arguments;
        }

        public static List<IRacketObject>? ParseRacketObjects(string str, Func<string, IRacketObject?> parseFunc)
        {
            string[] args = str.Split(" ");
            List<IRacketObject> arguments = new List<IRacketObject>();

            string predicate = "";
            for (int i = 0; i < args.Length; i++)
            {
                string currentToken = predicate + args[i];
                IRacketObject? newRacketObject = parseFunc.Invoke(currentToken) ?? RacketExpression.Parse(currentToken);

                if (newRacketObject != null)
                {
                    arguments.Add(newRacketObject);
                    predicate = "";
                    continue;
                }
                predicate += $"{args[i]} ";
            }

            if (predicate != "")
            {
                return null;
            }
            return arguments;
        }
    }
}
