using CodeZero.Configuration.Models;

namespace CodeZero.Configuration;

/// <summary>
/// Represents ConnectionStrings configuration parameters
/// </summary>
public partial class ConnectionStrings
{
    public virtual Cassandra Cassandra { get; set; } = default!;
    public virtual CouchBase CouchBase { get; set; } = default!;
    public virtual CouchDB CouchDB { get; set; } = default!;
    public virtual CosmosDB CosmosDB { get; set; } = default!;
    public virtual MariaDB MariaDB { get; set; } = default!;
    public virtual MongoDB MongoDB { get; set; } = default!;
    public virtual MySql MySql { get; set; } = default!;
    public virtual PostgreSql PostgreSql { get; set; } = default!;
    public virtual SqlServer SqlServer { get; set; } = default!;
}