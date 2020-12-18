namespace CodeZero
{
    /// <summary>
    /// Used to define some constants for Application.
    /// </summary>
    public static partial class AppConsts
    {
        public class HttpContentMediaTypes
        {
            public const string JSON = "application/json";
            public const string PDF = "application/pdf";
            public const string Image_JPEG = "image/jpeg";
            public const string Image_PNG = "image/png";
            public const string Image_SVG = "image/svg+xml";
            public const string Multipart_Mixed = "multipart/mixed";
            public const string Multipart = "multipart/form-data";
            public const string Text = "text/plain";
            public const string Text_CSS = "text/css";
            public const string Text_CSV = "text/csv";
            public const string Text_HTML = "text/html";
            public const string Video = "video/mp4";
        }

        public static class Orms
        {
            public const string EFCore = "EntityFrameworkCore";
            public const string Dapper = "Dapper";
            public const string NHibernate = "NHibernate";
        }

        public sealed class DatabaseType
        {
            public const string SQLServer = "SQLSERVER";
            public const string MySQL = "MYSQL";
            public const string MariaDB = "MARIADB";
            public const string PostgreSQL = "POSTGRESQL";
            public const string MongoDB = "MONGODB";
            public const string CouchDB = "COUCHDB";
        }

        public sealed class SQLServerConfig
        {
            public const string Host = "SQLSERVER";
            public const string Port = "MSSQL_TCP_PORT";
            public const string Database = "SQLSERVER_DATABASE_NAME";
            public const string UserName = "SQL_SERVER_USERNAME";
            public const string Password = "SQL_SERVER_PASSWORD";
        }

        public sealed class MySQLConfig
        {
            public const string Host = "MYSQLSERVER";
            public const string Port = "MYSQL_PORT";
            public const string Database = "MYSQL_DATABASE_NAME";
            public const string UserName = "MYSQL_USERNAME";
            public const string Password = "MYSQL_PASSWORD";
        }

        public sealed class MariaDBConfig
        {
            public const string Host = "MARIADB";
            public const string Port = "MARIADB_PORT";
            public const string Database = "MARIADB_DATABASE_NAME";
            public const string UserName = "MARIADB_USERNAME";
            public const string Password = "MARIADB_PASSWORD";
        }

        public sealed class PostgreSQLConfig
        {
            public const string Host = "POSTGRESQL_HOST";
            public const string Port = "POSTGRESQL_PORT";
            public const string Database = "POSTGRESQL_DATABASE_NAME";
            public const string UserName = "POSTGRESQL_USERNAME";
            public const string Password = "POSTGRESQL_PASSWORD";
        }

        public sealed class MongoDBConfig
        {
            public const string Host = "MONGODB_HOST";
            public const string Port = "MONGODB_PORT";
            public const string Database = "MONGODB_DATABASE_NAME";
            public const string UserName = "MONGODB_USERNAME";
            public const string Password = "MONGODB_PASSWORD";
        }

        public sealed class CouchDBConfig
        {
            public const string Host = "COUCHDB_HOST";
            public const string Port = "COUCHDB_PORT";
            public const string Database = "COUCHDB_DATABASE_NAME";
            public const string UserName = "COUCHDB_USERNAME";
            public const string Password = "COUCHDB_PASSWORD";
        }

        public static class RoleName
        {
            /// <summary>
            /// Supper Administrator Role
            /// </summary>
            public const string SupperAdmin = "SupperAdmin";

            /// <summary>
            /// Administrator Role
            /// </summary>
            public const string Admin = "Admin";

            /// <summary>
            /// Support Role
            /// </summary>
            public const string Support = "Support";

            /// <summary>
            /// Customer Role
            /// </summary>
            public const string Customer = "Customer";

            /// <summary>
            /// Company Role
            /// </summary>
            public const string Company = "Company";

            /// <summary>
            /// Individual Role
            /// </summary>
            public const string Individual = "Individual";

            /// <summary>
            /// Service Provider Role
            /// </summary>
            public const string ServiceProvider = "ServiceProvider";
        }
    }
}
