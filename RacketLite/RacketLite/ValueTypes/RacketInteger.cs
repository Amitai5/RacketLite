namespace RacketLite.ValueTypes
{
    public class RacketInteger : RacketNumber
    {
        public bool IsNatural => IsExact && Value > 0;

        public RacketInteger(long value, bool isExact)
            : base(value, isExact, true)
        {

        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public new static RacketInteger? Parse(string str)
        {
            bool isExact = true;
            if (str.StartsWith(RacketParsingHelper.InexactNumberPrefix))
            {
                str = str.Remove(0, 2);
                isExact = false;
            }

            if (int.TryParse(str, out int intValue))
            {
                return new RacketInteger(intValue, isExact);
            }
            return null;
        }
    }
}
