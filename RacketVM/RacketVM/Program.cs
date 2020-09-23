using RacketVM.Exceptions;
using RacketVM.Operands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RacketVM
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set up console window
            Console.Title = "Racket-Lite";
            Console.SetWindowSize(128, 36);
            Console.WriteLine("Welcome to Racket-Lite v1.0 [cs].");

            string response;
            do
            {
                Console.Write("> ");
                response = Console.ReadLine();
                response = Regex.Replace(response.Trim(), @"\s+", " ");

                //Catch missing parethesis
                int balanced = response.IsBalancedParenthesis();
                if (balanced > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The expression is missing one or more '(' or ')'.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                else if (balanced < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The expression has an extra one or more '(' or ')'.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                try
                {
                    Expression MainExpression = new Expression(response, null);
                    DynamicOperand expressionResult = MainExpression.Evaluate();
                    if (expressionResult == null)
                    {
                        continue;
                    }

                    //Check if it returned a string
                    if(expressionResult.Type == OperandType.String)
                    {
                        Console.WriteLine($"\"{expressionResult}\"");
                    }
                    else
                    {
                        Console.WriteLine(expressionResult);
                    }
                }

                //Catch Racket Exceptions
                catch (RacketVMException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exception.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (response.ToLower().Trim() != "exit");
        }
    }
}
