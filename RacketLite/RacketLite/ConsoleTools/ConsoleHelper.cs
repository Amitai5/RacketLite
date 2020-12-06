using System;
using System.Collections.Generic;
using System.IO;

namespace RacketLite.ConsoleTools
{
    public class ConsoleHelper : IConsoleReader
    {
        public ConsoleColor DefaultForeground { get; }
        public ConsoleColor DefaultBackground { get; }

        #region Write Functions

        /// <summary>
        /// Writes text to the console screen
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <param name="textColor">The color to display the text in</param>
        public IConsoleReader Write(string text, ConsoleColor? textColor = null)
        {
            if (!textColor.HasValue)
            {
                textColor = DefaultForeground;
            }

            Console.ForegroundColor = textColor.Value;
            Console.Write(text);
            return this;
        }

        /// <summary>
        /// Writes text to the console screen and terminates the line
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <param name="textColor">The color to display the text in</param>
        public IConsoleReader WriteLine(string text, ConsoleColor? textColor = null)
        {
            if (!textColor.HasValue)
            {
                textColor = DefaultForeground;
            }

            Console.ForegroundColor = textColor.Value;
            Console.WriteLine(text);
            return this;
        }

        #endregion

        #region Read Functions

        /// <summary>
        /// Reads text from the console and returns the result
        /// </summary>
        /// <param name="answerColor">The color the user should enter the text in</param>
        /// <returns>The string text read from the console</returns>
        public string? ReadText(ConsoleColor answerColor)
        {
            Console.ForegroundColor = answerColor;
            return Console.ReadLine();
        }

        /// <summary>
        /// Reads an integer from the console and returns the result
        /// </summary>
        /// <param name="answerColor">The color the user should enter the integer in</param>
        /// <param name="errorText">The text to display when someone inputs invalid text</param>
        /// <returns>The integer read from the console</returns>
        public int ReadValidInt(ConsoleColor answerColor, string errorText = "Please enter a valid integer...")
        {
            Console.ForegroundColor = answerColor;
            int startPos = Console.CursorLeft;
            string? text = Console.ReadLine();
            int textAsInt;

            while (!int.TryParse(text, out textAsInt))
            {
                Console.SetCursorPosition(startPos, Console.CursorTop - 1);
                Console.Write(new String(' ', Console.BufferWidth));

                Console.CursorLeft = 0;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(errorText);

                Console.SetCursorPosition(startPos, Console.CursorTop - 1);
                Console.ForegroundColor = answerColor;
                text = Console.ReadLine();
            }

            Console.Write(new string(' ', Console.BufferWidth));
            Console.CursorLeft = 0;
            Console.CursorTop--;

            return textAsInt;
        }

        /// <summary>
        /// Reads a double from the console and returns the result
        /// </summary>
        /// <param name="answerColor">The color the user should enter the double in</param>
        /// <param name="errorText">The text to display when someone inputs invalid text</param>
        /// <returns>The double read from the console</returns>
        public double ReadValidDouble(ConsoleColor answerColor, string errorText = "Please enter a valid number...")
        {
            Console.ForegroundColor = answerColor;
            int startPos = Console.CursorLeft;
            string? text = Console.ReadLine();
            double textAsDouble;

            while (!double.TryParse(text, out textAsDouble))
            {
                Console.SetCursorPosition(startPos, Console.CursorTop - 1);
                Console.Write(new String(' ', Console.BufferWidth));

                Console.CursorLeft = 0;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(errorText);

                Console.SetCursorPosition(startPos, Console.CursorTop - 1);
                Console.ForegroundColor = answerColor;
                text = Console.ReadLine();
            }

            Console.Write(new String(' ', Console.BufferWidth));
            Console.CursorLeft = 0;
            Console.CursorTop--;

            return textAsDouble;
        }

        /// <summary>
        /// Reads a valid file path from the console and returns the result
        /// </summary>
        /// <param name="answerColor">The color the user should enter the file path in</param>
        /// <returns>The file path read from the console</returns>
        public string ReadFilename(ConsoleColor answerColor)
        {
            return GetAutoCompletedFileName(answerColor);
        }

        /// <summary>
        /// Reads a valid file path from the console and returns the result
        /// </summary>
        /// <param name="answerColor">The color the user should enter the file path in</param>
        /// <param name="fileStream">The file stream of the file read by the console</param>
        /// <returns>The file path read from the console</returns>
        public string ReadFilename(ConsoleColor answerColor, out FileStream fileStream)
        {
            string filePath = GetAutoCompletedFileName(answerColor);
            fileStream = File.Open(filePath, FileMode.Open);
            return filePath;
        }

        #endregion

        #region Auto Complete

        private string GetAutoCompletedFileName(ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            string currentSearch = $@"C:\Users\{Environment.UserName}";
            List<string> possibleDirects = GetPossibleOptions(currentSearch, "");
            Console.Write(currentSearch);
            possibleDirects.Sort();

            //Start Looping For Keys
            ConsoleKeyInfo keyPressed;
            int tabPressIndex = 0;
            do
            {
                while (!Console.KeyAvailable) { }
                keyPressed = Console.ReadKey();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.Tab:
                        ClearConsoleLine(0);
                        possibleDirects = GetPossibleOptions(currentSearch.Substring(0, currentSearch.LastIndexOf('\\')), currentSearch.Substring(currentSearch.LastIndexOf('\\') + 1));

                        if (tabPressIndex >= possibleDirects.Count)
                        {
                            tabPressIndex = 0;
                        }

                        if (possibleDirects.Count > 0)
                        {
                            Console.ForegroundColor = textColor;
                            Console.Write(possibleDirects[tabPressIndex]);
                            tabPressIndex++;
                        }
                        else if (possibleDirects.Count == 0)
                        {
                            Console.ForegroundColor = textColor;
                            Console.Write(currentSearch);
                        }
                        break;

                    case ConsoleKey.Backspace:
                        if (tabPressIndex > 0)
                        {
                            tabPressIndex = 0;
                        }
                        else if (currentSearch.Length > 8)
                        {
                            currentSearch = currentSearch.Remove(currentSearch.Length - 1, 1);
                        }

                        ClearConsoleLine(0);
                        Console.ForegroundColor = textColor;
                        Console.Write(currentSearch);
                        break;

                    case ConsoleKey.Enter:
                        if (tabPressIndex > 0)
                        {
                            currentSearch = possibleDirects[tabPressIndex - 1];
                        }

                        Console.ForegroundColor = textColor;
                        Console.WriteLine(currentSearch);
                        return currentSearch;

                    //Handle the "\" key
                    case ConsoleKey.Oem5:
                        if (tabPressIndex > 0)
                        {
                            currentSearch = possibleDirects[tabPressIndex - 1];
                            tabPressIndex = 0;
                        }

                        if (!Directory.Exists(currentSearch))
                        {
                            ClearConsoleLine(0);
                            Console.Write(currentSearch);
                            continue;
                        }

                        currentSearch += keyPressed.KeyChar;
                        possibleDirects = GetPossibleOptions(currentSearch, "");
                        break;

                    //Handle all other key inputs
                    default:
                        tabPressIndex = 0;
                        currentSearch += keyPressed.KeyChar;
                        break;
                }

            } while (keyPressed.Key != ConsoleKey.Enter);
            return "";
        }

        private List<string> GetPossibleOptions(string scopeDirectory, string fileNameStarter)
        {
            //Get Rid Of Garbage KeyCodes
            fileNameStarter = fileNameStarter.Replace("\0", "");
            string[]? direcNames = new string[0];
            string[]? fileNames = new string[0];

            if (Directory.Exists(scopeDirectory))
            {
                fileNames = Directory.GetFiles(scopeDirectory, $"{fileNameStarter}*.*");
                direcNames = Directory.GetDirectories(scopeDirectory, $"{fileNameStarter}*.*");
            }

            List<string> combined = new List<string>();
            combined.AddRange(direcNames);
            combined.AddRange(fileNames);
            return combined;
        }

        #endregion Auto Complete

        #region Window Functions

        /// <summary>
        /// Erases the specified line
        /// </summary>
        /// <param name="LineNumber">The line number you want to erase (line zero being the bottom most line)</param>
        public void ClearConsoleLine(int LineNumber)
        {
            int OrigionalPos = Console.CursorTop;
            Console.SetCursorPosition(0, LineNumber > 0 ? Console.CursorTop - LineNumber : Console.CursorTop + LineNumber);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, OrigionalPos);
        }

        /// <summary>
        /// Sets the current foreground and background colors to their defaults
        /// </summary>
        /// <param name="clearScreen">Boolean should clear screen</param>
        public void ResetColors(bool clearScreen = false)
        {
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;

            if (clearScreen)
            {
                Console.Clear();
            }
        }

        #endregion Window Functions

        public ConsoleHelper(ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.Green)
        {
            DefaultForeground = foreground;
            DefaultBackground = background;
            ResetColors(true);
        }
    }
}