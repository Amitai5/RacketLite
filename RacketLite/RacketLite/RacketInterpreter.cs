using AEapps.CoreLibrary.ConsoleTools;
using System.Collections.Generic;
using RacketLite.Oporators;
using System;
using RacketLite.Operands;
using RacketLite.Exceptions;
using System.IO;
using RacketLite.Parsing;

namespace RacketLite
{
    public class RacketInterpreter
    {
        private readonly ConsoleHelper Helper;
        private readonly List<string> FunctionSignatures;
        public const string RacketLiteVersion = "v2.2 beta";
        public string TitleMessage => $"Welcome to Racket-Lite {RacketLiteVersion} [cs].";

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
                case "#ldr":
                case "#readfile":
                case "#loadfile":
                    string fileLocation = Helper.ReadFilename(ConsoleColor.Green);
                    string fileInfo = File.ReadAllText(fileLocation);
                    ParseMultiline(fileInfo);
                    break;
                case "#cls":
                case "#clr":
                case "#clear":
                    Console.Clear();
                    Console.WriteLine(TitleMessage);
                    break;
            }
        }

        public void ParseMultiline(string multilinedExpression)
        {
            //Reset colors
            Helper.ResetColors();
            string[] expressions = multilinedExpression.Split('\n');

            //Parse each individual expression
            for (int i = 3; i < expressions.Length; i++)
            {
                string currentExpression = expressions[i];
                if (string.IsNullOrWhiteSpace(currentExpression) || expressions[i].StartsWith(';'))
                {
                    //Ignore comments
                    continue;
                }

                //Parse multi-line expressions
                while (currentExpression.ParenthesisBalance() == 1 && i < expressions.Length - 1)
                {
                    i++;
                    currentExpression += expressions[i];
                }

                //Parse the expression
                ParseSingleLine(currentExpression, i - 2);
            }
        }

        public void ParseSingleLine(string expressionText, int lineNumber = -1)
        {
            try
            {
                //Reset the current stack vars each command
                StaticsManager.ResetCurrentStackVars();

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
                if (lineNumber == -1)
                {
                    Console.WriteLine(exception.Message);
                }
                else
                {
                    Console.WriteLine($"Line {lineNumber}: {exception.Message}");
                }
                Console.ForegroundColor = ConsoleColor.White;
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
    }
}
