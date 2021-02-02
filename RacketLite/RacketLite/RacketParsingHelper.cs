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


        public static List<IRacketObject>? ParseAny(string str, Dictionary<string, IRacketObject> localVars)
        {
            string[] args = str.Split(" ");
            List<IRacketObject> arguments = new List<IRacketObject>();

            string predicate = "";
            for (int i = 0; i < args.Length; i++)
            {
                IRacketObject? newRacketObject;
                string currentToken = predicate + args[i];
                if (ConstantValueDefinitions.UserDefinedConstants.ContainsKey(currentToken))
                {
                    newRacketObject = ConstantValueDefinitions.UserDefinedConstants[currentToken];
                }
                else if(localVars.ContainsKey(currentToken))
                {
                    newRacketObject = localVars[currentToken];
                }
                else
                {
                    newRacketObject = RacketBoolean.Parse(currentToken) ?? RacketNumber.Parse(currentToken)
                        ?? RacketString.Parse(currentToken) ?? (IRacketObject?)RacketExpression.Parse(currentToken, localVars);
                }

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

        public static List<IRacketObject>? ParseAny(string str)
        {
            string[] args = str.Split(" ");
            List<IRacketObject> arguments = new List<IRacketObject>();

            string predicate = "";
            for (int i = 0; i < args.Length; i++)
            {
                IRacketObject? newRacketObject;
                string currentToken = predicate + args[i];
                if (ConstantValueDefinitions.UserDefinedConstants.ContainsKey(currentToken))
                {
                    newRacketObject = ConstantValueDefinitions.UserDefinedConstants[currentToken];
                }
                else
                {
                    newRacketObject = RacketBoolean.Parse(currentToken) ?? RacketNumber.Parse(currentToken)
                        ?? RacketString.Parse(currentToken) ?? (IRacketObject?)RacketExpression.Parse(currentToken);
                }

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

        public static void ValidateParamTypes(Type expectedType, List<IRacketObject>? args)
        {
            if (args == null)
            {
                throw new ContractViolationException(expectedType, null);
            }

            foreach (IRacketObject argument in args)
            {
                ValidateParamType(expectedType, argument);
            }
        }

        public static void ValidateParamType(Type expectedType, IRacketObject argument)
        {
            if(argument is RacketVoid)
            {
                throw new ContractViolationException(expectedType, null);
            }

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
