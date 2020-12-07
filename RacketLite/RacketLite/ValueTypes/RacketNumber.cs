using System.Text;
using System;

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

        public new static RacketNumber? Parse(string str)
        {
            bool isExact = true;
            if (str.StartsWith(RacketParsingHelper.InexactNumberPrefix))
            {
                str = str.Remove(0, 2);
                isExact = false;
            }

            if (int.TryParse(str, out int intValue) && isExact)
            {
                return new RacketInteger(intValue);
            }

            if (float.TryParse(str, out float floatValue))
            {
                return new RacketFloat(floatValue, isExact, true);
            }
            return null;
        }

        public static RacketNumber Parse(float value, bool isExact, bool isRational)
        {
            if (value == Math.Floor(value) && isExact && isRational)
            {
                return new RacketInteger((long)value);
            }
            return new RacketFloat(value, isExact, isRational);
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(Value).Append('\n');
        }
    }
}
