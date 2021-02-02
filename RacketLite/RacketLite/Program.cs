using System;

namespace RacketLite
{
    internal static class Program
    {
        private static void Main()
        {
            //Set up console window
            Console.Title = "Racket-Lite";
            RacketInterpreter interpreter = new RacketInterpreter(true, false);

            while(true)
            {
                interpreter.ReadAndParseLine();
            }
        }
    }
}