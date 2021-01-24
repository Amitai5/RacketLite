using System.Text;

namespace RacketLite.ValueTypes
{
    public abstract class RacketValueType : IRacketObject
    {
        public abstract void ToTreeString(StringBuilder stringBuilder, int tabIndex);

        public RacketValueType Evaluate()
        {
            return this;
        }

        public static RacketValueType? Parse(string str)
        {
            RacketValueType? valueType = RacketNumber.Parse(str);
            if (valueType != null)
            {
                return valueType;
            }

            valueType = RacketString.Parse(str);
            if (valueType != null)
            {
                return valueType;
            }
            return null;
        }
    }
}