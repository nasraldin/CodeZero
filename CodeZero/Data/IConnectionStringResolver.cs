using JetBrains.Annotations;
using System.Threading.Tasks;

namespace CodeZero.Data
{
    public interface IConnectionStringResolver
    {
        [NotNull]
        Task<string> ResolveAsync(string connectionStringName = null);
    }

    public static class ConnectionStringResolverExtensions
    {
        [NotNull]
        public static Task<string> ResolveAsync<T>(this IConnectionStringResolver resolver)
        {
            return resolver.ResolveAsync(ConnectionStringNameAttribute.GetConnStringName<T>());
        }
    }
}
