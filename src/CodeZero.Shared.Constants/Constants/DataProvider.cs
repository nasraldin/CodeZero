namespace CodeZero;

public static partial class AppConsts
{
    public static partial class DataProvider
    {
        public sealed class SqlServer
        {
            public const string Host = "SQLSERVER_HOST";
            public const string Port = "MSSQL_TCP_PORT";
            public const string Database = "SQLSERVER_DATABASE_NAME";
            public const string UserName = "SQLSERVER_USERNAME";
            public const string Password = "SQLSERVER_PASSWORD";
        }

        public sealed class CosmosDB
        {
            public const string Account = "COSMOSDB_ACCOUNT";
            public const string Key = "COSMOSDB_KEY";
            public const string DatabaseName = "COSMOSDB_DATABASE_NAME";
            public const string ContainerName = "COSMOSDB_CONTAINER_NAME";
        }

        public sealed class MySql
        {
            public const string Host = "MYSQL_HOST";
            public const string Port = "MYSQL_PORT";
            public const string Database = "MYSQL_DATABASE_NAME";
            public const string UserName = "MYSQL_USERNAME";
            public const string Password = "MYSQL_PASSWORD";
        }

        public sealed class MariaDB
        {
            public const string Host = "MARIADB_HOST";
            public const string Port = "MARIADB_PORT";
            public const string Database = "MARIADB_DATABASE_NAME";
            public const string UserName = "MARIADB_USERNAME";
            public const string Password = "MARIADB_PASSWORD";
        }

        public sealed class PostgreSql
        {
            public const string Host = "POSTGRESQL_HOST";
            public const string Port = "POSTGRESQL_PORT";
            public const string Database = "POSTGRESQL_DATABASE_NAME";
            public const string UserName = "POSTGRESQL_USERNAME";
            public const string Password = "POSTGRESQL_PASSWORD";
        }

        public sealed class MongoDB
        {
            public const string Host = "MONGODB_HOST";
            public const string Port = "MONGODB_PORT";
            public const string Database = "MONGODB_DATABASE_NAME";
            public const string UserName = "MONGODB_USERNAME";
            public const string Password = "MONGODB_PASSWORD";
        }

        public sealed class CouchDB
        {
            public const string Host = "COUCHDB_HOST";
            public const string Port = "COUCHDB_PORT";
            public const string Database = "COUCHDB_DATABASE_NAME";
            public const string UserName = "COUCHDB_USERNAME";
            public const string Password = "COUCHDB_PASSWORD";
        }

        public sealed class CouchBase
        {
            public const string Host = "COUCHBASE_HOST";
            public const string Port = "COUCHBASE_PORT";
            public const string Database = "COUCHBASE_DATABASE_NAME";
            public const string UserName = "COUCHBASE_USERNAME";
            public const string Password = "COUCHBASE_PASSWORD";
        }
    }
}