using AEapps.CoreLibrary.ConsoleTools;
using RacketLite.Expressions;
using System.Text;
using System;

namespace RacketLite
{
    public class RacketInterpreter
    {
        private readonly ConsoleHelper Helper;
        public const string RacketLiteVersion = "v3.0 beta";
        public string TitleMessage => $"Welcome to Racket-Lite {RacketLiteVersion} [cs].";

        public RacketInterpreter()
        {
            Helper = new ConsoleHelper(ConsoleColor.Black, ConsoleColor.White);
            Console.WriteLine(TitleMessage);
        }

        public void Parse(string str)
        {
            try
            {
                RacketExpression expression = RacketExpression.Parse(str);
                expression.Evaluate();

                StringBuilder stringBuilder = new StringBuilder();
                expression.ToTreeString(stringBuilder, 0);
                string test = stringBuilder.ToString();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Helper.ResetColors();
            }
        }
    }
}