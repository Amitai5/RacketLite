using AEapps.CoreLibrary.ConsoleTools;
using RacketLite.Exceptions;
using RacketLite.Operands;
using System;
using System.Collections.Generic;
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
            RacketInterpreter interpreter = new RacketInterpreter();

            string expressionText;
            do
            {
                Console.Write("> ");
                expressionText = Console.ReadLine();
                expressionText = Regex.Replace(expressionText, @"\s+", " ").Trim();

                //Parse Racket-Lite Interpreter Directives
                if(expressionText.StartsWith('#'))
                {
                    interpreter.ParseDirective(expressionText);
                    continue;
                }

                //Parse Racket-Lite Expressions
                interpreter.ParseExpression(expressionText);

            } while (expressionText.ToLower().Trim() != "exit");
        }
    }
}
