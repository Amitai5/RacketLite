using System.Text;

namespace RacketLite.ValueTypes
{
    public class RacketBoolean : RacketValueType
    {
        public bool Value { get; init; }

        public RacketBoolean(bool value)
        {
            Value = value;
        }

        public new static RacketBoolean? Parse(string str)
        {
            str = str.ToLower();
            if (ConstantValueDefinitions.BooleanDefinitions.ContainsKey(str))
            {
                return ConstantValueDefinitions.BooleanDefinitions[str];
            }
            return null;
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(this).Append('\n');
        }

        public override string ToString()
        {
            return $"#{(Value ? "t" : "f")}";
        }
    }
}
