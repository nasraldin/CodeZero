namespace System;

/// <summary>
/// Extension methods for String class.
/// </summary>
public static partial class StringExtensions
{
    #region Private
    /// <summary>
    /// Determines whether the given IList object [is null or empty].
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns><c>true</c> if the given IList object [is null or empty]; 
    /// otherwise, <c>false</c>.</returns>
    private static bool IsNullOrEmpty<T>(this IList<T> obj)
    {
        return obj == null || obj.Count == 0;
    }
    #endregion

    #region IsNullOrEmpty
    /// <summary>
    /// Indicates whether this string is null or an System.String.Empty string.
    /// </summary>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// Indicates whether this string is null, empty, or consists only of white-space characters.
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// Determines whether [is not null or empty] [the specified input].
    /// </summary>
    /// <returns>
    /// <c>true</c> if [is not null or empty] [the specified input]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNotNullOrEmpty(this string input)
    {
        return !string.IsNullOrEmpty(input);
    }

    /// <summary>
    /// Replaces NULL with the specified replacement value.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="alt"></param>
    /// <returns></returns>
    public static string IsNullThen(this string str, string alt)
    {
        return str ?? alt ?? string.Empty;
    }
    #endregion

    #region Manipulation
    /// <summary>
    /// Adds a char to beginning of given string if it does not starts with the char.
    /// </summary>
    public static string EnsureStartsWith(
        this string str,
        char c,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.StartsWith(c.ToString(), comparisonType))
        {
            return str;
        }

        return c + str;
    }

    /// <summary>
    /// Adds a char to end of given string if it does not ends with the char.
    /// </summary>
    public static string EnsureEndsWith(
        this string str,
        char c,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.EndsWith(c.ToString(), comparisonType))
        {
            return str;
        }

        return str + c;
    }

    /// <summary>
    /// Gets a substring of a string from end of the string.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    /// <exception cref="ArgumentException">Thrown if 
    /// <paramref name="len"/> is bigger that string's length</exception>
    public static string Right(this string str, int len)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.Length < len)
        {
            throw new ArgumentException("len argument can not be bigger than given string's length!");
        }

        return str.Substring(str.Length - len, len);
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    /// <exception cref="ArgumentException">Thrown if 
    /// <paramref name="len"/> is bigger that string's length</exception>
    public static string Left(this string str, int len)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.Length < len)
        {
            throw new ArgumentException("len argument can not be bigger than given string's length!");
        }

        return str[..len];
    }

    /// <summary>
    /// Gets index of nth occurrence of a char in a string.
    /// </summary>
    /// <param name="str">source string to be searched</param>
    /// <param name="c">Char to search in str</param>
    /// <param name="n">Count of the occurrence</param>
    public static int NthIndexOf(this string str, char c, int n)
    {
        ArgumentNullException.ThrowIfNull(str);

        var count = 0;

        for (var i = 0; i < str.Length; i++)
        {
            if (str[i] != c)
            {
                continue;
            }

            if ((++count) == n)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Uses string.Split method to split given string by given separator.
    /// </summary>
    public static string[] Split(this string str, string separator)
    {
        return str.Split(new[] { separator }, StringSplitOptions.None);
    }

    /// <summary>
    /// Uses string.Split method to split given string by given separator.
    /// </summary>
    public static string[] Split(this string str, string separator, StringSplitOptions options)
    {
        return str.Split(new[] { separator }, options);
    }

    /// <summary>
    /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
    /// </summary>
    public static string[] SplitToLines(this string str)
    {
        return str.Split(Environment.NewLine);
    }

    /// <summary>
    /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
    /// </summary>
    public static string[] SplitToLines(this string str, StringSplitOptions options)
    {
        return str.Split(Environment.NewLine, options);
    }

    /// <summary>
    /// Returns an enumerable collection of the specified type 
    /// containing the substrings in this instance that are 
    /// delimited by elements of a specified Char array
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="separator">
    /// An array of Unicode characters that delimit the substrings 
    /// in this instance, an empty array containing no delimiters, or null.
    /// </param>
    /// <typeparam name="T">
    /// The type of the elemnt to return in the collection, this type must implement IConvertible.
    /// </typeparam>
    /// <returns>
    /// An enumerable collection whose elements contain the substrings 
    /// in this instance that are delimited by one or more characters in separator. 
    /// </returns>
    public static IEnumerable<T> SplitTo<T>(
        this string str,
        params char[] separator)
        where T : IConvertible
    {
        foreach (var s in str.Split(separator, StringSplitOptions.None))
            yield return (T)Convert.ChangeType(s, typeof(T));
    }

    /// <summary>
    /// Used when we want to completely remove HTML code and not encode it with XML entities.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string StripHtml(this string str)
    {
        return Regex.Replace(str, @"<(.|\n)*?>", string.Empty);
    }
    #endregion

    #region Converts
    /// <summary>
    /// Converts line endings in the string to <see cref="Environment.NewLine"/>.
    /// </summary>
    public static string NormalizeLineEndings(this string str)
    {
        return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
    }

    /// <summary>
    /// Converts string to enum value.
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="str">String value to convert</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string str) where T : struct
    {
        ArgumentNullException.ThrowIfNull(str);

        return (T)Enum.Parse(typeof(T), str);
    }

    /// <summary>
    /// Converts string to enum value.
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="str">String value to convert</param>
    /// <param name="ignoreCase">Ignore case</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string str, bool ignoreCase) where T : struct
    {
        ArgumentNullException.ThrowIfNull(str);

        return (T)Enum.Parse(typeof(T), str, ignoreCase);
    }

    /// <summary>
    /// Converts PascalCase string to camelCase string.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="useCurrentCulture">set true to use current culture.
    /// Otherwise, invariant culture will be used.</param>
    /// <returns>camelCase of the string</returns>
    public static string ToCamelCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }
        if (str.Length == 1)
        {
            return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        return (useCurrentCulture ? char.ToLower(str[0]) : char.ToLowerInvariant(str[0])) + str[1..];
    }

    /// <summary>
    /// Converts camelCase string to PascalCase string.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="useCurrentCulture">set true to use current culture.
    /// Otherwise, invariant culture will be used.</param>
    /// <returns>PascalCase of the string</returns>
    public static string ToPascalCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }
        if (str.Length == 1)
        {
            return useCurrentCulture ? str.ToUpper() : str.ToUpperInvariant();
        }

        return (useCurrentCulture ? char.ToUpper(str[0]) : char.ToUpperInvariant(str[0])) + str[1..];
    }

    /// <summary>
    /// Converts a string to pascal case.
    /// </summary>
    public static string ToPascalCase(this string attribute, char upperAfterDelimiter)
    {
        var nextIsUpper = true;
        attribute = attribute.Trim();
        var result = new StringBuilder(attribute.Length);

        foreach (var c in attribute)
        {
            if (c == upperAfterDelimiter)
            {
                nextIsUpper = true;
                continue;
            }
            if (nextIsUpper)
            {
                result.Append(char.ToUpperInvariant(c));
            }
            else
            {
                result.Append(c);
            }

            nextIsUpper = false;
        }

        return result.ToString();
    }

    /// <summary>
    /// Converts given PascalCase/camelCase string to sentence (by splitting words by space).
    /// Example: "ThisIsSampleSentence" is converted to "This is a sample sentence".
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="useCurrentCulture">set true to use current culture.
    /// Otherwise, invariant culture will be used.</param>
    public static string ToSentenceCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return useCurrentCulture
            ? Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]))
            : Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLowerInvariant(m.Value[1]));
    }

    /// <summary>
    /// Converts given PascalCase/camelCase string to kebab-case.
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="useCurrentCulture">set true to use current culture.
    /// Otherwise, invariant culture will be used.</param>
    public static string ToKebabCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        str = str.ToCamelCase();

        return useCurrentCulture
            ? Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLower(m.Value[1]))
            : Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLowerInvariant(m.Value[1]));
    }

    public static string Capitalize(this string str)
    {
        return char.ToUpper(str[0]) + str[1..];
    }

    public static string ToKeywords(this string str)
    {
        return Regex.Replace(Regex.Replace(Regex.Replace(str, @"[\W]{1,}", " "), @"\b[\w]{0,3}\b", string.Empty).Trim(), @"[\W]{1,}", ",");
    }

    /// <summary>
    /// Converts string to a Name-Format where each first letter is Uppercase.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns></returns>
    public static string ToUpperLowerNameVariant(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        char[] valuearray = value.ToLower().ToCharArray();
        bool nextupper = true;

        for (int i = 0; i < (valuearray.Length - 1); i++)
        {
            if (nextupper)
            {
                valuearray[i] = char.Parse(valuearray[i].ToString().ToUpper());
                nextupper = false;
            }
            else
            {
                nextupper = valuearray[i] switch
                {
                    ' ' or '-' or '.' or ':' or '\n' => true,
                    _ => false,
                };
            }
        }

        return new string(valuearray);
    }

    public static string ToSHA256(this string str)
    {
        var sb = new StringBuilder();

        using (SHA256 hash = SHA256.Create())
        {
            var enc = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = hash.ComputeHash(enc);

            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("X2"));
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Converts given string to a byte array using <see cref="Encoding.UTF8"/> encoding.
    /// </summary>
    public static byte[] GetBytes(this string str)
    {
        return str.GetBytes(Encoding.UTF8);
    }

    /// <summary>
    /// Converts given string to a byte array using the given <paramref name="encoding"/>
    /// </summary>
    public static byte[] GetBytes([NotNull] this string str, [NotNull] Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(str);
        ArgumentNullException.ThrowIfNull(encoding);

        return encoding.GetBytes(str);
    }

    /// <summary>
    /// Convert hex String to bytes representation
    /// </summary>
    /// <param name="hexString">Hex string to convert into bytes</param>
    /// <returns>Bytes of hex string</returns>
    public static byte[] HexToBytes(this string hexString)
    {
        if (hexString.Length % 2 != 0)
            throw new ArgumentException(string.Format("HexString cannot be in odd number: {0}", hexString));

        var retVal = new byte[hexString.Length / 2];

        for (int i = 0; i < hexString.Length; i += 2)
            retVal[i / 2] = byte.Parse(hexString.AsSpan(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

        return retVal;
    }

    public static byte[] ToByteArray(this string hex)
    {
        return Enumerable.Range(0, hex.Length).
            Where(x => 0 == x % 2).
            Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).
            ToArray();
    }

    public static string GetValueOrEmpty(this string value)
    {
        return GetValueOrDefault(value, string.Empty);
    }

    public static string GetValueOrDefault(this string value, string defaultvalue)
    {
        if (value != null) return value;
        return defaultvalue;
    }
    #endregion

    #region ModifiedString
    public static string ReplaceFirst(
        this string str,
        string search,
        string replace,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(str);

        var pos = str.IndexOf(search, comparisonType);

        if (pos < 0)
        {
            return str;
        }

        return str[..pos] + replace + str[(pos + search.Length)..];
    }

    /// <summary>
    /// Trims or removes duplicate delimited characters and leave 
    /// only one instance of that character.
    /// charactertrim like , | :
    /// </summary>
    /// <param name="str"></param>
    /// <param name="charactertrim"></param>
    /// <returns></returns>
    public static string TrimDuplicates(this string str, char charactertrim)
    {
        return str.TrimCharacter(charactertrim);
    }

    /// <summary>
    /// Trims or removes duplicate delimited characters and leave 
    /// only one instance of that character.
    /// charactertrim like , | :
    /// </summary>
    /// <param name="str"></param>
    /// <param name="character"></param>
    /// <returns></returns>
    private static string TrimCharacter(this string str, char character)
    {
        string result = string.Empty;
        result = string.Join(character.ToString(), str.Split(character).Where(s => s != string.Empty).ToArray());

        return result;
    }

    /// <summary>
    /// Should extract title (file name) from file path or Url
    /// </summary>
    /// <param name="str">path\to\file.ext</param>
    /// <returns>fileName.ext</returns>
    public static string ExtractFileName(this string str)
    {
        if (str.Contains('\\'))
        {
            return string.IsNullOrWhiteSpace(str) ? string.Empty :
                str[str.LastIndexOf("\\")..].Replace("\\", "");
        }
        else if (str.Contains('/'))
        {
            return string.IsNullOrWhiteSpace(str) ? string.Empty :
                str[str.LastIndexOf("/")..].Replace("/", "");
        }
        else
        {
            return str;
        }
    }

    /// <summary>
    /// Converts title to valid URL slug
    /// </summary>
    /// <param name="str">Title</param>
    /// <returns>Slug</returns>
    public static string ToSlug(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        str = str.ToLowerInvariant();
        var bytes = Encoding.GetEncoding("utf-8").GetBytes(str);
        str = Encoding.ASCII.GetString(bytes);
        str = Regex.Replace(str, @"\s", "-", RegexOptions.Compiled);
        str = Regex.Replace(str, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
        str = str.Trim('-', '_');
        str = Regex.Replace(str, @"([-_]){2,}", "$1", RegexOptions.Compiled);

        return str;
    }

    /// <summary>
    /// Converts post body to post description
    /// </summary>
    /// <param name="str">HTML post body</param>
    /// <param name="len">The text length, defualt 300</param>
    /// <returns>Post decription as plain text</returns>
    public static string ToDescription(this string str, int len = 300)
    {
        str = str.StripHtml();

        return str.Length > len ? str[..len] : str;
    }

    public static string ReplaceIgnoreCase(this string str, string search, string replacement)
    {
        string result = Regex.Replace(
            str,
            Regex.Escape(search),
            replacement.Replace("$", "$$"),
            RegexOptions.IgnoreCase
        );

        return result;
    }
    #endregion

    #region WordPart
    /// <summary>
    /// Removes first occurrence of the given postfixes from end of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="postFixes">one or more postfix.</param>
    /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
    public static string RemovePostFix(this string str, params string[] postFixes)
    {
        return str.RemovePostFix(StringComparison.Ordinal, postFixes);
    }

    /// <summary>
    /// Removes first occurrence of the given postfixes from end of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="comparisonType">String comparison type</param>
    /// <param name="postFixes">one or more postfix.</param>
    /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
    public static string RemovePostFix(
        this string str,
        StringComparison comparisonType,
        params string[] postFixes)
    {
        if (str.IsNullOrEmpty())
        {
            return string.Empty;
        }
        if (postFixes.IsNullOrEmpty())
        {
            return str;
        }

        var pf = from postFix in postFixes
                 where str.EndsWith(postFix, comparisonType)
                 select postFix.FirstOrDefault();

        if (pf.Any())
        {
            return str.Left(str.Length - pf.Count());
        }

        return str;
    }

    /// <summary>
    /// Removes first occurrence of the given prefixes from beginning of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="preFixes">one or more prefix.</param>
    /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
    public static string RemovePreFix(this string str, params string[] preFixes)
    {
        return str.RemovePreFix(StringComparison.Ordinal, preFixes);
    }

    /// <summary>
    /// Removes first occurrence of the given prefixes from beginning of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="comparisonType">String comparison type</param>
    /// <param name="preFixes">one or more prefix.</param>
    /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
    public static string RemovePreFix(
        this string str,
        StringComparison comparisonType,
        params string[] preFixes)
    {
        if (str.IsNullOrEmpty())
        {
            return string.Empty;
        }
        if (preFixes.IsNullOrEmpty())
        {
            return str;
        }

        //foreach (var preFix in preFixes)
        //{
        //    if (str.StartsWith(preFix, comparisonType))
        //    {
        //        return str.Right(str.Length - preFix.Length);
        //    }
        //}

        var pf = from preFixe in preFixes
                 where str.EndsWith(preFixe, comparisonType)
                 select preFixe.FirstOrDefault();

        if (pf.Any())
        {
            return str.Left(str.Length - pf.Count());
        }

        return str;
    }
    #endregion

    #region Truncate
    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if 
    /// <paramref name="str"/> is null</exception>
    public static string Truncate(this string str, int maxLength)
    {
        if (str.IsNotNullOrEmpty())
        {
            return string.Empty;
        }
        if (str.Length <= maxLength)
        {
            return str;
        }

        return str.Left(maxLength);
    }

    /// <summary>
    /// Gets a substring of a string from Ending of the string if it exceeds maximum length.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if 
    /// <paramref name="str"/> is null</exception>
    public static string TruncateFromBeginning(this string str, int maxLength)
    {
        if (str.IsNotNullOrEmpty())
        {
            return string.Empty;
        }
        if (str.Length <= maxLength)
        {
            return str;
        }

        return str.Right(maxLength);
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// It adds a "..." postfix to end of the string if it's truncated.
    /// Returning string can not be longer than maxLength.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if 
    /// <paramref name="str"/> is null</exception>
    public static string TruncateWithPostfix(this string str, int maxLength)
    {
        return TruncateWithPostfix(str, maxLength, "...");
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// It adds given <paramref name="postfix"/> to end of the string if it's truncated.
    /// Returning string can not be longer than maxLength.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if 
    /// <paramref name="str"/> is null</exception>
    public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
    {
        if (str.IsNotNullOrEmpty() || maxLength == 0)
        {
            return string.Empty;
        }
        if (str.Length <= maxLength)
        {
            return str;
        }
        if (maxLength <= postfix.Length)
        {
            return postfix.Left(maxLength);
        }

        return str.Left(maxLength - postfix.Length) + postfix;
    }
    #endregion

    #region Checks
    /// <summary>
    /// Count all words in a given string
    /// </summary>
    /// <param name="input">string to begin with</param>
    /// <returns>int</returns>
    public static int WordCount(this string input)
    {
        var count = 0;
        try
        {
            // Exclude whitespaces, Tabs and line breaks
            var re = new Regex(@"[^\s]+");
            var matches = re.Matches(input);
            count = matches.Count;
        }
        catch
        {
            Console.WriteLine($"WordCount: catch input {input}");
        }

        return count;
    }

    /// <summary>
    /// Checks string object's value to array of string values.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="stringValues">Array of string values to compare</param>
    /// <returns>Return true if any string value matches</returns>
    public static bool In(this string value, params string[] stringValues)
    {
        foreach (string otherValue in stringValues)
        {
            if (string.Compare(value, otherValue) == 0)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Compare a string using SQL Like style wildcards
    /// </summary>
    /// <param name="value">string to be searched</param>
    /// <param name="searchString">the search string containing wildcards</param>
    /// <returns>value.Like(searchString)</returns>
    /// <example>value.Like("a")</example>
    /// <example>value.Like("a%")</example>
    /// <example>value.Like("%b")</example>
    /// <example>value.Like("a%b")</example>
    /// <example>value.Like("a%b%c")</example>
    /// seealso https://stackoverflow.com/questions/1040380/wildcard-search-for-linq
    public static bool Like(this string value, string searchString)
    {
        bool result = false;
        var likeParts = searchString.Split(new char[] { '%' });

        for (int i = 0; i < likeParts.Length; i++)
        {
            if (likeParts[i] == string.Empty)
            {
                continue;   // "a%"
            }
            if (i == 0)
            {
                if (likeParts.Length == 1) // "a"
                {
                    result = value.Equals(likeParts[i], StringComparison.OrdinalIgnoreCase);
                }
                else // "a%" or "a%b"
                {
                    result = value.StartsWith(likeParts[i], StringComparison.OrdinalIgnoreCase);
                }
            }
            else if (i == likeParts.Length - 1) // "a%b" or "%b"
            {
                result &= value.EndsWith(likeParts[i], StringComparison.OrdinalIgnoreCase);
            }
            else // "a%b%c"
            {
                int current = value.IndexOf(likeParts[i], StringComparison.OrdinalIgnoreCase);
                int previous = value.IndexOf(likeParts[i - 1], StringComparison.OrdinalIgnoreCase);
                result &= previous < current;
            }
        }

        return result;
    }

    /// <summary>
    /// String containing SQL Like style wildcards to be ReverseLike another string 
    /// </summary>
    /// <param name="value">search string containing wildcards</param>
    /// <param name="compareString">string to be compared</param>
    /// <returns>value.ReverseLike(compareString)</returns>
    /// <example>value.ReverseLike("a")</example>
    /// <example>value.ReverseLike("abc")</example>
    /// <example>value.ReverseLike("ab")</example>
    /// <example>value.ReverseLike("axb")</example>
    /// <example>value.ReverseLike("axbyc")</example>
    /// <remarks>reversed logic of Like String extension</remarks>
    public static bool ReverseLike(this string value, string compareString)
    {
        bool result = false;
        var likeParts = value.Split(new char[] { '%' });

        for (int i = 0; i < likeParts.Length; i++)
        {
            if (likeParts[i] == string.Empty)
            {
                continue;   // "a%"
            }
            if (i == 0)
            {
                if (likeParts.Length == 1) // "a"
                {
                    result = compareString.Equals(likeParts[i], StringComparison.OrdinalIgnoreCase);
                }
                else // "a%" or "a%b"
                {
                    result = compareString.StartsWith(likeParts[i], StringComparison.OrdinalIgnoreCase);
                }
            }
            else if (i == likeParts.Length - 1) // "a%b" or "%b"
            {
                result &= compareString.EndsWith(likeParts[i], StringComparison.OrdinalIgnoreCase);
            }
            else // "a%b%c"
            {
                int current = compareString.IndexOf(likeParts[i], StringComparison.OrdinalIgnoreCase);
                int previous = compareString.IndexOf(likeParts[i - 1], StringComparison.OrdinalIgnoreCase);
                result &= previous < current;
            }
        }

        return result;
    }

    /// <summary>
    /// Determines if specified string is DateTime.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsDate(this string str)
    {
        return DateTime.TryParse(str, out _);
    }
    #endregion

    #region Compress
    /// <summary>
    /// Compress string using Gzip.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string CompressString(this string text)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(text);
        var memoryStream = new MemoryStream();

        using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
        {
            gZipStream.Write(buffer, 0, buffer.Length);
        }

        memoryStream.Position = 0;
        var compressedData = new byte[memoryStream.Length];
        memoryStream.Read(compressedData, 0, compressedData.Length);
        var gZipBuffer = new byte[compressedData.Length + 4];
        Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
        Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);

        return Convert.ToBase64String(gZipBuffer);
    }

    /// <summary>
    /// Decompress string using Gzip.
    /// </summary>
    /// <param name="compressedText"></param>
    /// <returns></returns>
    public static string DecompressString(string compressedText)
    {
        byte[] gZipBuffer = Convert.FromBase64String(compressedText);
        using var memoryStream = new MemoryStream();
        int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
        memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);
        var buffer = new byte[dataLength];
        memoryStream.Position = 0;

        using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
        {
            gZipStream.Read(buffer, 0, buffer.Length);
        }

        return Encoding.UTF8.GetString(buffer);
    }
    #endregion

    #region Format
    /// <summary>
    /// Formats a string with one literal placeholder.
    /// </summary>
    /// <param name="text">The extension text</param>
    /// <param name="arg0">Argument 0</param>
    /// <returns>The formatted string</returns>
    public static string FormatWith(this string text, object arg0)
    {
        return string.Format(text, arg0);
    }

    /// <summary>
    /// Formats a string with two literal placeholders.
    /// </summary>
    /// <param name="text">The extension text</param>
    /// <param name="arg0">Argument 0</param>
    /// <param name="arg1">Argument 1</param>
    /// <returns>The formatted string</returns>
    public static string FormatWith(this string text, object arg0, object arg1)
    {
        return string.Format(text, arg0, arg1);
    }

    /// <summary>
    /// Formats a string with tree literal placeholders.
    /// </summary>
    /// <param name="text">The extension text</param>
    /// <param name="arg0">Argument 0</param>
    /// <param name="arg1">Argument 1</param>
    /// <param name="arg2">Argument 2</param>
    /// <returns>The formatted string</returns>
    public static string FormatWith(
        this string text,
        object arg0,
        object arg1,
        object arg2)
    {
        return string.Format(text, arg0, arg1, arg2);
    }

    /// <summary>
    /// Formats a string with a list of literal placeholders.
    /// </summary>
    /// <param name="text">The extension text</param>
    /// <param name="args">The argument list</param>
    /// <returns>The formatted string</returns>
    public static string FormatWith(this string text, params object[] args)
    {
        return string.Format(text, args);
    }

    /// <summary>
    /// Formats a string with a list of literal placeholders.
    /// </summary>
    /// <param name="text">The extension text</param>
    /// <param name="provider">The format provider</param>
    /// <param name="args">The argument list</param>
    /// <returns>The formatted string</returns>
    public static string FormatWith(
        this string text,
        IFormatProvider provider,
        params object[] args)
    {
        return string.Format(provider, text, args);
    }

    /// <summary>
    /// Replaces the format item in a specified System.String with 
    /// the text equivalent of the value of a specified System.Object instance.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="arg">The arg</param>
    /// <param name="additionalArgs">The additional args</param>
    /// <returns></returns>
    public static string Format(
        this string format,
        object arg,
        params object[] additionalArgs)
    {
        if (additionalArgs is null || additionalArgs.Length == 0)
        {
            return string.Format(format, arg);
        }
        else
        {
            return string.Format(format, new object[] { arg }.Concat(additionalArgs).ToArray());
        }
    }
    #endregion

    #region ToXConversions
    /// <summary>
    /// Convert to the integer.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="defaultvalue">The defaultvalue.</param>
    /// <returns></returns>
    public static int ToInteger(this string value, int defaultvalue)
    {
        return (int)ToDouble(value, defaultvalue);
    }

    /// <summary>
    /// Convert to the integer.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static int ToInteger(this string value)
    {
        return ToInteger(value, 0);
    }

    /// <summary>
    ///  Check string input IsDigit
    /// </summary>
    /// <param name="str"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string OnlyNumbers(this string str, string input)
    {
        return new string(input.Where(char.IsDigit).ToArray());
    }

    /// <summary>
    /// Convert to the double.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="defaultvalue">The defaultvalue.</param>
    /// <returns></returns>
    public static double ToDouble(this string value, double defaultvalue)
    {
        if (double.TryParse(value, out double result))
        {
            return result;
        }
        else
        {
            return defaultvalue;
        }
    }

    /// <summary>
    /// Convert to the double.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static double ToDouble(this string value)
    {
        return ToDouble(value, 0);
    }

    /// <summary>
    /// Convert to the date time.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="defaultvalue">The defaultvalue.</param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string value, DateTime? defaultvalue)
    {
        if (DateTime.TryParse(value, out DateTime result))
        {
            return result;
        }
        else
        {
            return defaultvalue;
        }
    }

    /// <summary>
    /// Convert to the date time.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string value)
    {
        return ToDateTime(value, null);
    }

    /// <summary>
    /// Converts a string value to bool value, supports "T" and "F" conversions.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A bool based on the string value</returns>
    public static bool? ToBoolean(this string value)
    {
        if (string.Compare("T", value, true) == 0)
        {
            return true;
        }
        if (string.Compare("F", value, true) == 0)
        {
            return false;
        }
        if (bool.TryParse(value, out bool result))
        {
            return result;
        }
        else
        {
            return null;
        }
    }
    #endregion
}