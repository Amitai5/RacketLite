namespace RacketLite.ValueTypes
{
    public class RacketInteger : RacketNumber
    {
        public RacketInteger(long value)
            : base(value, true, true)
        {

        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
