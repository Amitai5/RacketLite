using RacketLite.ValueTypes;
using System.Text;

namespace RacketLite
{
    public interface IRacketObject
    {
        public RacketValueType Evaluate();

        public void ToTreeString(StringBuilder stringBuilder, int tabIndex);
    }
}