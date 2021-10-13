using System.Text;

namespace Digitteck.JDConverter.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhitespaces(this string value)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char ch in value)
            {
                if (!char.IsWhiteSpace(ch))
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }
    }
}
