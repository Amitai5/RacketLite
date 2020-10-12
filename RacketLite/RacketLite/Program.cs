using RacketLite.Exceptions;
using RacketLite.Operands;
using RacketLite.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RacketLite
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set up console window
            Console.Title = "Racket-Lite";
            Console.SetWindowSize(128, 36);
            Console.WriteLine("Welcome to Racket-Lite v2.1 [cs].");

            string expressionText;
            do
            {
                Console.Write("> ");
                expressionText = Console.ReadLine();
                expressionText = Regex.Replace(expressionText, @"\s+", " ").Trim();

                try
                {
                    //Clear the local stack each time we call
                    StaticsManager.LocalStack.Clear();

                    RacketExpression mainEx = new RacketExpression(expressionText);
                    DynamicOperand result = mainEx.Evaluate();
                    if (result != null)
                    {
                        Console.WriteLine(result.ToString());
                    }
                }

                //Catch Racket Exceptions
                catch (RacketException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exception.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (expressionText.ToLower().Trim() != "exit");
        }
    }
}
