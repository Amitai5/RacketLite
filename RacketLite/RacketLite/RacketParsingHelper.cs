using System.Collections.Generic;
using RacketLite.Expressions;
using RacketLite.ValueTypes;

namespace RacketLite
{
    public static class RacketParsingHelper
    {
        public const string InexactNumberPrefix = "#i";

        public static List<IRacketObject>? ParseRacketNumbers(string str)
        {
            string[] args = str.Split(" ");
            List<IRacketObject> arguments = new List<IRacketObject>();

            string predicate = "";
            for (int i = 0; i < args.Length; i++)
            {
                string currentToken = predicate + args[i];
                IRacketObject? newRacketObject = RacketNumber.Parse(currentToken) ?? (IRacketObject?)RacketExpression.Parse(currentToken);

                if (newRacketObject != null)
                {
                    arguments.Add(newRacketObject);
                    predicate = "";
                    continue;
                }
                predicate += $"{args[i]} ";
            }

            if(predicate != "")
            {
                return null;
            }
            return arguments;
        }

        public static List<IRacketObject>? ParseRacketIntegers(string str)
        {
            string[] args = str.Split(" ");
            List<IRacketObject> arguments = new List<IRacketObject>();

            string predicate = "";
            for (int i = 0; i < args.Length; i++)
            {
                string currentToken = predicate + args[i];
                IRacketObject? newRacketObject = RacketInteger.Parse(currentToken) ?? (IRacketObject?)RacketExpression.Parse(currentToken);

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

        public static List<IRacketObject>? ParseRacketInteger(string str)
        {
            IRacketObject? number = RacketInteger.Parse(str) ?? (IRacketObject?)RacketExpression.Parse(str);
            if (number != null)
            {
                return new List<IRacketObject>() { number };
            }
            return null;
        }

        public static List<IRacketObject>? ParseRacketNumber(string str)
        {
            IRacketObject? number = RacketNumber.Parse(str) ?? (IRacketObject?)RacketExpression.Parse(str);
            if (number != null)
            {
                return new List<IRacketObject>() { number };
            }
            return null;
        }
    }
}
