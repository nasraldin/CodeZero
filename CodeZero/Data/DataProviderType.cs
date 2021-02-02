using System.Runtime.Serialization;

namespace CodeZero.Data
{
    /// <summary>
    /// Represents data provider type enumeration
    /// </summary>
    public enum DataProviderType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "")]
        Unknown,

        /// <summary>
        /// MS SQL Server
        /// </summary>
        [EnumMember(Value = "sqlserver")]
        SqlServer,

        /// <summary>
        /// MySQL
        /// </summary>
        [EnumMember(Value = "mysql")]
        MySql,

        /// <summary>
        /// MariaDB
        /// </summary>
        [EnumMember(Value = "mariadb")]
        MariaDB,

        /// <summary>
        /// PostgreSQL
        /// </summary>
        [EnumMember(Value = "postgresql")]
        PostgreSQL,

        /// <summary>
        /// MongoDB
        /// </summary>
        [EnumMember(Value = "mongodb")]
        MongoDB,

        /// <summary>
        /// CouchDB
        /// </summary>
        [EnumMember(Value = "couchdb")]
        CouchDB,
    }
}