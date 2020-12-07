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
            str = str.Replace("#", "").ToLower();
            return str switch
            {
                "false" => new RacketBoolean(false),
                "true" => new RacketBoolean(true),

                "f" => new RacketBoolean(false),
                "t" => new RacketBoolean(true),
                _ => null
            };
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(Value).Append('\n');
        }

        public override string ToString()
        {
            return $"#{(Value ? "t" : "f")}";
        }
    }
}
