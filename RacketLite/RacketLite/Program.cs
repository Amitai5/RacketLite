using RacketLite.ConsoleTools;
using RacketLite.ValueTypes;
using System;

namespace RacketLite
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //Set up console window
            Console.Title = "Racket-Lite";
            ConsoleHelper helper = new ConsoleHelper(ConsoleColor.Black, ConsoleColor.White);
            Console.WriteLine($"Welcome to Racket-Lite {RacketInterpreter.RacketLiteVersion} [cs].");

            string expressionText;
            do
            {
                Console.Write("> ");
                expressionText = Console.ReadLine() ?? "";

                //Parse Racket-Lite expressions
                RacketValueType? result = RacketInterpreter.ParseLine(expressionText);
                if(result != null)
                {
                    Console.WriteLine(result.ToString());
                    continue;
                }

                //Highlight the last line in red
                helper.ClearConsoleLine(1);
                Console.CursorTop--;
                Console.Write("> ");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(expressionText);
                helper.ResetColors();

            } while (expressionText.ToLower().Trim() != "exit");
        }
    }
}
