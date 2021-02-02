using System;
using System.Text;

namespace RacketLite.ValueTypes
{
    public abstract class RacketNumber : RacketValueType
    {
        public float Value { get; init; }
        public bool IsExact { get; init; }
        public bool IsRational { get; init; }

        public bool IsNatural => Value > 0 && IsExact && IsInteger;
        public bool IsInteger => Value == MathF.Floor(Value) && IsRational;

        protected RacketNumber(float value, bool exact, bool rational)
        {
            Value = value;
            IsExact = exact;
            IsRational = rational;
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(this).Append('\n');
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
            if (ConstantValueDefinitions.NumericDefinitions.ContainsKey(str))
            {
                return ConstantValueDefinitions.NumericDefinitions[str];
            }
            return RacketInteger.Parse(str) ?? (RacketNumber?)RacketFloat.Parse(str);
        }
    }
}
