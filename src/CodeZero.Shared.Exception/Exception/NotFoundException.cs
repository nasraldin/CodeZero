namespace CodeZero.Exception;

/// <summary>
/// Represents an error that occurs if a queried object by a particular key is null (not found).
/// </summary>
public class NotFoundException : CodeZeroException
{
    /// <summary>
    /// Initializes a new instance of the NotFoundException class 
    /// with a specified name of the queried object and its key.
    /// </summary>
    /// <param name="key">The value by which the object is queried.</param>
    /// <param name="objectName">Name of the queried object.</param>
    public NotFoundException(string key, string objectName)
        : base($"\"{objectName}\" ({key}) was not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the NotFoundException class 
    /// with a specified name of the queried object, its key,
    /// and the exception that is the cause of this exception.
    /// </summary>
    /// <param name="key">The value by which the object is queried.</param>
    /// <param name="objectName">Name of the queried object.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NotFoundException(string key, string objectName, System.Exception innerException)
        : base($"Queried object {objectName} was not found, Key: {key}", innerException)
    {
    }
}