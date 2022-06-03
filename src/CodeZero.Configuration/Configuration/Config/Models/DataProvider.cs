namespace CodeZero.Configuration.Models;

public partial class SqlServer : IDefaultConnection
{
    public string DefaultConnection { get; set; } = default!;
}

public partial class MySql : IDefaultConnection
{
    public string DefaultConnection { get; set; } = default!;
}

public partial class CosmosDB
{
    public string Account { get; set; } = default!;
    public string Key { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public string ContainerName { get; set; } = default!;
}

public partial class MariaDB : IDefaultConnection
{
    public string DefaultConnection { get; set; } = default!;
}

public partial class PostgreSql : IDefaultConnection
{
    public string DefaultConnection { get; set; } = default!;
}

public partial class MongoDB : IDefaultConnection
{
    public string DefaultConnection { get; set; } = default!;
}

public partial class Cassandra : IDefaultConnection
{
    public string DefaultConnection { get; set; } = default!;
}

public partial class CouchBase : IDefaultConnection
{
    public string DefaultConnection { get; set; } = default!;
}

public partial class CouchDB : IDefaultConnection
{
    public string DefaultConnection { get; set; } = default!;
}

public interface IDefaultConnection
{
    string DefaultConnection { get; set; }
}