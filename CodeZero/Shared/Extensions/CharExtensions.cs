namespace CodeZero.Shared.Extensions
{
    public static class CharExtensions
    {
        public static bool In(this char value, params char[] chars)
        {
            foreach (char c in chars)
                if (c == value)
                    return true;

            return false;
        }
    }
}
