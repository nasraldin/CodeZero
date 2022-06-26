using System.Diagnostics;
using System.Text.Json;
using JetBrains.Annotations;

namespace CodeZero;

[DebuggerStepThrough]
public static class Check
{
    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(
        T value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        ArgumentNullException.ThrowIfNull(parameterName);

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(
        T value,
        [InvokerParameterName][NotNull] string parameterName,
        string message)
    {
        if (value is null)
        {
            throw new ArgumentNullException(parameterName, message);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static string NotNull(
        string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength =
        int.MaxValue,
        int minLength = 0)
    {
        if (value is null)
        {
            throw new ArgumentException($"{parameterName} can not be null!", parameterName);
        }
        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
        }
        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static string NotNullOrEmpty(
        string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength =
        int.MaxValue,
        int minLength = 0)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);
        }
        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
        }
        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static ICollection<T> NotNullOrEmpty<T>(
        ICollection<T> value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value is null || value.Count <= 0)
        {
            throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
        }

        return value;
    }

    [ContractAnnotation("value:null => halt")]
    public static string NotNullOrWhiteSpace(
        string value, [InvokerParameterName][NotNull] string parameterName,
        int maxLength =
        int.MaxValue,
        int minLength = 0)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
        }
        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
        }
        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// Removes any special characters in the input string not provided in the allowed special character list. 
    /// </summary>
    /// <param name="input">Input string to process</param>
    /// <param name="allowedCharacters">list of allowed special characters </param>
    /// <returns></returns>
    public static string RemoveSpecialCharacters(string input, string allowedCharacters)
    {
        ArgumentNullException.ThrowIfNull(input);

        int index = 0;
        char[] buffer = new char[input.Length];
        char[] allowedSpecialCharacters = allowedCharacters.ToCharArray();

        foreach (char c in input.Where(c => char.IsLetterOrDigit(c) || allowedSpecialCharacters.Any(x => x == c)))
        {
            buffer[index] = c;
            index++;
        }

        return new string(buffer, 0, index);
    }

    /// <summary>
    /// Throws ArgumentNullException for null parameter
    /// </summary>
    /// <param name="parameter">Value to check against</param>
    /// <param name="message">Exception message</param>
    public static void CheckNull(object parameter, string message)
    {
        if (parameter is null)
            throw new ArgumentNullException(message);
    }

    /// <summary>
    /// Throws ArgumentNullException for null array
    /// </summary>
    /// <param name="parameter">Value to check against</param>
    /// <param name="message">Exception message</param>
    public static void CheckNull(Array parameter, string message)
    {
        if (parameter is null)
            throw new ArgumentNullException(message);
    }

    /// <summary>
    /// First check for Null and throws ArgumentNullException 
    /// otherwise throws ArgumentException for empty string
    /// </summary>
    /// <param name="parameter">Value to check against</param>
    /// <param name="message">Exception message</param>
    public static void CheckNullOrEmpty(string parameter, string message)
    {
        CheckNull(parameter, message);

        if (parameter.Length == 0)
            throw new ArgumentException(message, parameter);
    }

    /// <summary>
    /// First check for Null and throws ArgumentNullException 
    /// otherwise throws ArgumentException for empty array
    /// </summary>
    /// <param name="parameter">Value to check against</param>
    /// <param name="message">Exception message</param>
    public static void CheckNullOrEmpty(Array parameter, string message)
    {
        CheckNull(parameter, message);

        if (parameter.Length == 0)
            throw new ArgumentException(message);
    }

    /// <summary>
    /// First check for Null and throws ArgumentNullException 
    /// otherwise throws ArgumentException for white space or empty string
    /// </summary>
    /// <param name="parameter">Value to check against</param>
    /// <param name="message">Exception message</param>
    public static void CheckNullOrTrimEmpty(string parameter, string message)
    {
        CheckNull(parameter, message);

        if (parameter.Trim().Length == 0)
            throw new ArgumentException(message, parameter);
    }

    /// <summary>
    /// Check using string.IsNullOrWhiteSpace and throws ArgumentException
    /// </summary>
    /// <param name="parameter">Value to check against</param>
    /// <param name="message">Exception message</param>
    public static void CheckNullOrWhiteSpace(string parameter, string message)
    {
        if (string.IsNullOrWhiteSpace(parameter))
            throw new ArgumentException(message, parameter);
    }

    /// <summary>
    /// Check if a string is valid json.
    /// </summary>
    [ContractAnnotation("json:null => halt")]
    public static bool IsJson(string json)
    {
        if (string.IsNullOrEmpty(json))
            return false;

        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"IsJson ex: {ex}");

            return false;
        }
    }
}