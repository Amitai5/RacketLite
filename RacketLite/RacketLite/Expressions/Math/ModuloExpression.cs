using System.Collections.Generic;
using RacketLite.ValueTypes;

namespace RacketLite.Expressions
{
    public sealed class ModuloExpression : RacketExpression
    {
        private ModuloExpression(List<IRacketObject> args)
            : base("Modulo")
        {
            arguments = args;
        }

        public static new ModuloExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketIntegers(str);
            if (arguments?.Count > 0)
            {
                return new ModuloExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            RacketNumber currentNumber = (RacketNumber)arguments[0].Evaluate();
            float retValue = currentNumber.Value;
            bool isExact = currentNumber.IsExact;

            for (int i = 1; i < arguments.Count; i++)
            {
                currentNumber = (RacketNumber)arguments[i].Evaluate();
                isExact = isExact && currentNumber.IsExact;
                retValue %= currentNumber.Value;

            }
            return new RacketInteger((long)retValue, isExact);
        }
    }
}
