namespace RacketLite.ValueTypes
{
    public class RacketInteger : RacketNumber
    {
        public RacketInteger(int value)
            : base(value, true, true)
        {

        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
