using CodeZero.Shared.Extensions;
using CodeZero.Shared.Extensions.Collections;
using CodeZero.Shared.Extensions.Reflection;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CodeZero.Shared
{
    [DebuggerStepThrough]
    public static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(T value, [InvokerParameterName][NotNull] string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(T value, [InvokerParameterName][NotNull] string parameterName, string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNull(string value, [InvokerParameterName][NotNull] string parameterName, int maxLength = int.MaxValue, int minLength = 0)
        {
            if (value == null)
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
        public static string NotNullOrEmpty(string value, [InvokerParameterName][NotNull] string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, [InvokerParameterName][NotNull] string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNullOrEmpty(string value, [InvokerParameterName][NotNull] string parameterName, int maxLength = int.MaxValue, int minLength = 0)
        {
            if (value.IsNullOrEmpty())
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
        public static string NotNullOrWhiteSpace(string value, [InvokerParameterName][NotNull] string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNullOrWhiteSpace(string value, [InvokerParameterName][NotNull] string parameterName, int maxLength = int.MaxValue, int minLength = 0)
        {
            if (value.IsNullOrWhiteSpace())
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

        [ContractAnnotation("type:null => halt")]
        public static Type AssignableTo<TBaseType>(Type type, [InvokerParameterName][NotNull] string parameterName)
        {
            NotNull(type, parameterName);

            if (!type.IsAssignableTo<TBaseType>())
            {
                throw new ArgumentException($"{parameterName} (type of {type.AssemblyQualifiedName}) should be assignable to the {typeof(TBaseType).GetFullNameWithAssemblyName()}!");
            }

            return type;
        }

        public static string Length([CanBeNull] string value, [InvokerParameterName][NotNull] string parameterName, int maxLength, int minLength = 0)
        {
            if (minLength > 0)
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
                }

                if (value.Length < minLength)
                {
                    throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
                }
            }

            if (value != null && value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
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
            char[] buffer = new char[input.Length];
            int index = 0;

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
            if (parameter == null)
                throw new ArgumentNullException(message);
        }

        /// <summary>
        /// Throws ArgumentNullException for null array
        /// </summary>
        /// <param name="parameter">Value to check against</param>
        /// <param name="message">Exception message</param>
        public static void CheckNull(Array parameter, string message)
        {
            if (parameter == null)
                throw new ArgumentNullException(message);
        }

        /// <summary>
        /// First check for Null and throws ArgumentNullException otherwise throws ArgumentException for empty string
        /// </summary>
        /// <param name="parameter">Value to check against</param>
        /// <param name="message">Exception message</param>
        public static void CheckNullOrEmpty(string parameter, string message)
        {
            CheckNull(parameter, message);

            if (parameter.Length == 0)
                throw new ArgumentException(message);
        }

        /// <summary>
        /// First check for Null and throws ArgumentNullException otherwise throws ArgumentException for white space or empty string
        /// </summary>
        /// <param name="parameter">Value to check against</param>
        /// <param name="message">Exception message</param>
        public static void CheckNullOrTrimEmpty(string parameter, string message)
        {
            CheckNull(parameter, message);

            if (parameter.Trim().Length == 0)
                throw new ArgumentException(message);
        }

        /// <summary>
        /// Check using string.IsNullOrWhiteSpace and throws ArgumentException
        /// </summary>
        /// <param name="parameter">Value to check against</param>
        /// <param name="message">Exception message</param>
        public static void CheckNullOrWhiteSpace(string parameter, string message)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                throw new ArgumentException(message);
        }

        /// <summary>
        /// First check for Null and throws ArgumentNullException otherwise throws ArgumentException for empty array
        /// </summary>
        /// <param name="parameter">Value to check against</param>
        /// <param name="message">Exception message</param>
        public static void CheckNullOrEmpty(Array parameter, string message)
        {
            CheckNull(parameter, message);

            if (parameter.Length == 0)
                throw new ArgumentException(message);
        }
    }
}
