using System.Linq;

namespace CodeZero.Utils
{
    public static class StringUtils
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static string OnlyNumbers(this string str, string input)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}