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

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(Value).Append('\n');
        }

        public static RacketNumber Parse(float value, bool isExact, bool isRational)
        {
            if (value == Math.Floor(value) && isRational)
            {
                return new RacketInteger((long)value, isExact);
            }
            return new RacketFloat(value, isExact, isRational);
        }

        public new static RacketNumber? Parse(string str)
        {
            return RacketInteger.Parse(str) ?? (RacketNumber?)RacketFloat.Parse(str);
        }
    }
}
