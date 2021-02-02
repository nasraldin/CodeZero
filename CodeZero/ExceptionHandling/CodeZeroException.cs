using System;
using System.Runtime.Serialization;

namespace CodeZero.ExceptionHandling
{
    /// <summary>
    /// Base exception type for those are thrown by CodeZero system for app specific exceptions.
    /// </summary>
    public class CodeZeroException : Exception
    {
        /// <inheritdoc />
        public CodeZeroException()
        {
        }

        /// <inheritdoc />
        public CodeZeroException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="messageFormat">The exception message format.</param>
        /// <param name="args">The exception message arguments.</param>
        public CodeZeroException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <inheritdoc />
        public CodeZeroException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        public CodeZeroException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}