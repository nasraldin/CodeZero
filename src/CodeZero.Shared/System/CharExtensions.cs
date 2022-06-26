namespace System;

public static class CharExtensions
{
    public static bool In(this char value, params char[] chars)
    {
        foreach (char c in chars)
        {
            if (c == value)
                return true;
        }

        return false;
    }
}

public static class ByteExtensions
{
    public static string ToHexString(this byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "");
    }
}