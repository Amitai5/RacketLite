using System.Text;

namespace RacketLite
{
    public interface IRacketObject
    {
        public string GetSignature();

        public void ToTreeString(StringBuilder stringBuilder, int tabIndex);
    }
}