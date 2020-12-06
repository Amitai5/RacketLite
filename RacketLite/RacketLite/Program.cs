using System;

namespace RacketLite
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //Set up console window
            Console.Title = "Racket-Lite";
            RacketInterpreter interpreter = new RacketInterpreter();

            string expressionText;
            do
            {
                Console.Write("> ");
                expressionText = Console.ReadLine();

                //Parse Racket-Lite Expressions
                interpreter.Parse(expressionText);

            } while (expressionText.ToLower().Trim() != "exit");
        }
    }
}
