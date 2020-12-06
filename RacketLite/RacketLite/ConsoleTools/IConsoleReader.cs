using System;

namespace RacketLite.ConsoleTools
{
    public interface IConsoleReader
    {
        public string? ReadText(ConsoleColor answerColor);
        public int ReadValidInt(ConsoleColor answerColor, string errorText = "Please enter a valid number...");
        public double ReadValidDouble(ConsoleColor answerColor, string errorText = "Please enter a valid number...");
    }
}