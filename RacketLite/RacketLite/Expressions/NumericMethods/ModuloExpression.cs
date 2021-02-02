using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class ModuloExpression : NumericExpression
    {
        private ModuloExpression(List<IRacketObject> args)
            : base("Modulo")
        {
            parameters = args;
        }

        public static ModuloExpression? Parse(List<IRacketObject>? parameters)
        {
            RacketParsingHelper.ValidateParamTypes(typeof(RacketInteger), parameters);
            if (parameters?.Count > 0)
            {
                return new ModuloExpression(parameters);
            }
            return null;
        }

        public override RacketInteger Evaluate()
        {
            RacketInteger currentNumber = (RacketInteger)parameters[0].Evaluate();
            float retValue = currentNumber.Value;
            bool isExact = currentNumber.IsExact;

            for (int i = 1; i < parameters.Count; i++)
            {
                currentNumber = (RacketInteger)parameters[i].Evaluate();
                isExact = isExact && currentNumber.IsExact;
                retValue %= currentNumber.Value;
            }
            return new RacketInteger((long)retValue, isExact);
        }
    }
}
