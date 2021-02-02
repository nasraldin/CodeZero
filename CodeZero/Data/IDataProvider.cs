namespace CodeZero.Interfaces
{
    /// <summary>
    /// Data provider interface
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        ///     Initialize connection factory
        /// </summary>
        void InitConnectionFactory();

        /// <summary>
        ///     Set database initializer
        /// </summary>
        void SetDatabaseInitializer();

        /// <summary>
        ///     Initialize database
        /// </summary>
        void InitDatabase();
    }
}