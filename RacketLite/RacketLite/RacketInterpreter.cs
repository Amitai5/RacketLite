using AEapps.CoreLibrary.ConsoleTools;
using System.Collections.Generic;
using RacketLite.Oporators;
using System;
using RacketLite.Operands;
using RacketLite.Exceptions;

namespace RacketLite
{
    public class RacketInterpreter
    {
        private readonly ConsoleHelper Helper;
        private readonly List<string> FunctionSignatures;
        public const string TitleMessage = "Welcome to Racket-Lite v2.1 [cs].";

        public RacketInterpreter()
        {
            Helper = new ConsoleHelper(ConsoleColor.Black, ConsoleColor.White);
            FunctionSignatures = OporatorDefinitions.GetFunctionSignatures();
            Console.WriteLine(TitleMessage);
        }

        public void ParseDirective(string directiveText)
        {
            switch (directiveText.ToLower())
            {
                case "#help":
                    PrintSignatures(FunctionSignatures);
                    break;
                case "#cls":
                case "#clr":
                    Console.Clear();
                    Console.WriteLine(TitleMessage);
                    break;
            }
        }

        private void PrintSignatures(List<string> functionSignatures)
        {
            //Print out title
            Console.WriteLine("\n  Racket Function Definitions: \n");
            for (int i = 0; i < functionSignatures.Count; i++)
            {
                //Space it a bit from the wall
                Console.BackgroundColor = Helper.DefaultBackground;
                Console.Write("  ");

                //Switch colors every other line
                if (Console.ForegroundColor == ConsoleColor.White)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }

                //Write the function signature
                Console.WriteLine(functionSignatures[i]);
            }
            Helper.ResetColors();
            Console.Write("\n");
        }

        public void ParseExpression(string expressionText)
        {
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
        }
    }
}
