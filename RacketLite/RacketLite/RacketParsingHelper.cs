using System.Collections.Generic;
using RacketLite.Expressions;
using RacketLite.Exceptions;
using RacketLite.ValueTypes;
using System;

namespace RacketLite
{
    public static class RacketParsingHelper
    {
        public const string InexactNumberPrefix = "#i";

        public static List<IRacketObject>? ParseRacketBooleans(string str)
        {
            List<IRacketObject>? args = ParseAny(str.Trim());
            ValidateReturnTypes(typeof(RacketBoolean), args);
            return args;
        }

        public static List<IRacketObject>? ParseRacketStrings(string str)
        {
            List<IRacketObject>? args = ParseAny(str.Trim());
            ValidateReturnTypes(typeof(RacketString), args);
            return args;
        }

        public static List<IRacketObject>? ParseRacketNumbers(string str)
        {
            List<IRacketObject>? args = ParseAny(str.Trim());
            ValidateReturnTypes(typeof(RacketNumber), args);
            return args;
        }

        public static List<IRacketObject>? ParseRacketIntegers(string str)
        {
            List<IRacketObject>? args = ParseAny(str.Trim());
            ValidateReturnTypes(typeof(RacketInteger), args);
            return args;
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

        public static void ValidateReturnTypes(Type expectedType, List<IRacketObject>? args)
        {
            if (args == null)
            {
                throw new ContractViolationException(expectedType, null);
            }

            foreach (IRacketObject argument in args)
            {
                ValidateReturnType(expectedType, argument);
            }
        }

        public static void ValidateReturnType(Type expectedType, IRacketObject argument)
        {
            if (argument is RacketExpression expression && expression.ReturnType != null)
            {
                if (!expression.ReturnType.IsAssignableFrom(expectedType) && !expression.ReturnType.IsSubclassOf(expectedType))
                {
                    throw new ContractViolationException(expectedType, expression.ReturnType);
                }
            }
            else if (!argument.GetType().IsAssignableFrom(expectedType) && !argument.GetType().IsSubclassOf(expectedType))
            {
                throw new ContractViolationException(expectedType, argument.GetType());
            }
        }
    }
}
