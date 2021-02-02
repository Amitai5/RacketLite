using System.Text;

namespace RacketLite.ValueTypes
{
    public class RacketVoid : RacketValueType
    {
        public override void ToTreeString(StringBuilder stringBuilder, int tabIndex)
        {
            stringBuilder.Append('\t', tabIndex);
            stringBuilder.Append("None").Append('\n');
        }

        public override string ToString()
        {
            return "";
        }
    }
}
