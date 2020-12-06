using System.Linq;
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
            if(str.StartsWith('"') && str.EndsWith('"'))
            {
                return new RacketString(str[1..^1]);
            }
            return null;
        }

        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append(Value).Append('\n');
        }
    }
}
