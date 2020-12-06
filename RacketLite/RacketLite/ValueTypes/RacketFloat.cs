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
            return $"{(IsExact ? "" : "#i")}{Value:.0################}";
        }
    }
}
