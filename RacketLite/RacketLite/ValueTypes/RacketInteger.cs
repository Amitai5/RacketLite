namespace RacketLite.ValueTypes
{
    public class RacketInteger : RacketNumber
    {
        public RacketInteger(float value, bool exact)
            : base(value, exact, true)
        {

        }
    }
}
