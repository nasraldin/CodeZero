using System;
using System.Runtime.Serialization;

namespace CodeZero.Exceptions
{
    /// <summary>
    ///     Base exception type for those are thrown by CodeZero system for app specific exceptions.
    /// </summary>
    public class CodeZeroException : Exception
    {
        public CodeZeroException()
        {
        }

        public CodeZeroException(string message)
            : base(message)
        {
        }

        public CodeZeroException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CodeZeroException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}