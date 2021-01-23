using RacketLite.ValueTypes;
using System.Collections.Generic;

namespace RacketLite.Expressions
{
    public sealed class IfExpression : RacketExpression
    {
        private IfExpression(List<IRacketObject> args)
            : base("If")
        {
            arguments = args;
        }

        public static new IfExpression? Parse(string str)
        {
            List<IRacketObject>? arguments = RacketParsingHelper.ParseRacketAny(str);
            if (arguments?.Count == 3 && arguments[0].Evaluate() is RacketBoolean) //TODO: fix this using parent expressions. Ex: MathExpression, StringExpression, BooleanExpression based on what they return
            {
                return new IfExpression(arguments);
            }
            return null;
        }

        public override RacketValueType Evaluate()
        {
            bool questionValue = ((RacketBoolean)arguments[0].Evaluate()).Value;
            if(questionValue)
            {
                return arguments[1].Evaluate();
            }
            return arguments[2].Evaluate();
        }
    }
}
