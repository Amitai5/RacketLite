using System.Collections.Generic;
using RacketLite.Expressions;
using RacketLite.ValueTypes;
using System;

namespace RacketLite
{
    public static class RacketParsingHelper
    {
        public const string InexactNumberPrefix = "#i";

        public static List<IRacketObject>? ParseRacketBooleans(string str)
        {
            return Parse(str, RacketBoolean.Parse, BooleanExpression.Parse);
        }

        public static List<IRacketObject>? ParseRacketStrings(string str)
        {
            return Parse(str, RacketString.Parse, BooleanExpression.Parse);
        }

        public static List<IRacketObject>? ParseRacketNumbers(string str)
        {
            return Parse(str, RacketNumber.Parse, NumericExpression.Parse);
        }

        public static List<IRacketObject>? ParseAny(string str)
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

        public static List<IRacketObject>? Parse(string str, Func<string, IRacketObject?> valueTypeParser, Func<string, IRacketObject?>? expressionParser = null)
        {
            string[] args = str.Split(" ");
            List<IRacketObject> arguments = new List<IRacketObject>();

            string predicate = "";
            for (int i = 0; i < args.Length; i++)
            {
                string currentToken = predicate + args[i];
                IRacketObject? newRacketObject = valueTypeParser.Invoke(currentToken) ?? expressionParser?.Invoke(currentToken) ?? SpecialExpression.Parse(currentToken);

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

        public static List<IRacketObject>? ParseSpecific(string str, params (Func<string, IRacketObject?>, Func<string, IRacketObject?>)[] objectParsers)
        {
            string[] args = str.Split(" ");
            List<IRacketObject> arguments = new List<IRacketObject>();

            string predicate = "";
            int parsedObjectCounter = 0;
            for (int i = 0; i < args.Length; i++)
            {
                string currentToken = predicate + args[i];

                (Func<string, IRacketObject?>, Func<string, IRacketObject?>) currentParsers = objectParsers[parsedObjectCounter];
                IRacketObject? newRacketObject = currentParsers.Item1.Invoke(currentToken) ?? currentParsers.Item2.Invoke(currentToken);

                if (newRacketObject != null)
                {
                    arguments.Add(newRacketObject);
                    parsedObjectCounter++;
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
