namespace RacketLite.ValueTypes
{
    public class RacketFloat : RacketNumber
    {
        public RacketFloat(float value, bool exact, bool rational)
            : base(value, exact, rational)
        {

        }

        public override string ToString()
        {
            return $"{(IsExact ? "" : "#i")}{Value:0.0################}";
        }

        public new static RacketFloat? Parse(string str)
        {
            bool isExact = true;
            if (str.StartsWith(RacketParsingHelper.InexactNumberPrefix))
            {
                str = str.Remove(0, 2);
                isExact = false;
            }

            if (float.TryParse(str, out float floatValue))
            {
                return new RacketFloat(floatValue, isExact, true);
            }
            return null;
        }
    }
}
