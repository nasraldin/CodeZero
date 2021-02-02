namespace CodeZero.Domain.Entities
{
    /// <summary>
    /// Concurrency control or management the consistency of the data when more than one user is accessing it for different purposes.
    /// </summary>
    public interface IHasConcurrencyStamp
    {
        /// <summary>
        /// Stores the version stamp of the data. A new Row version value is added each time a user updates the data.
        /// </summary>
        byte[] RowVersion { get; set; }
    }
}