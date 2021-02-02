using System.Text;

namespace RacketLite.ValueTypes
{
    public class RacketString : RacketValueType
    {
        public string Value { get; init; }

        public RacketString(string value)
        {
            Value = value;
        }

        public new static RacketString? Parse(string str)
        {
            if(str.StartsWith('"') && str.EndsWith('"') && str.Length >= 2)
            {
                return new RacketString(str[1..^1]);
            }
            else if(ConstantValueDefinitions.StringDefinitions.ContainsKey(str))
            {
                return ConstantValueDefinitions.StringDefinitions[str];
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
            return $"\"{Value}\"";
        }
    }
}
