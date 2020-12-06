using System.Text;

namespace RacketLite.ValueTypes
{
    public abstract class RacketNumber : RacketValueType
    {
        public float Value { get; init; }
        public bool IsExact { get; init; }
        public bool IsRational { get; init; }

        protected RacketNumber(float value, bool exact, bool rational)
        {
            Value = value;
            IsExact = exact;
            IsRational = rational;
        }

        public new static RacketNumber Parse(string str)
        {
            bool isExact = str.Contains(SyntaxRules.InexactNumberPrefix);
            str = str.Replace(SyntaxRules.InexactNumberPrefix, "");

            if (float.TryParse(str, out float floatValue))
            {
                return new RacketFloat(floatValue, isExact, true);
            }

            if (int.TryParse(str, out int intValue))
            {
                return new RacketInteger(intValue, isExact);
            }
            return null;
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(Value).Append('\n');
        }

        public override string GetSignature()
        {
            return "[Number]";
        }
    }
}
