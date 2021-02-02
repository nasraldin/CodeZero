using CodeZero.Shared.Extensions;
using CodeZero.Shared.Extensions.Collections;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CodeZero.Data
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver
    {
        protected CodeZeroDbConnectionOptions Options { get; }

        public DefaultConnectionStringResolver(IOptionsSnapshot<CodeZeroDbConnectionOptions> options)
        {
            Options = options.Value;
        }

        public virtual Task<string> ResolveAsync(string connectionStringName = null)
        {
            return Task.FromResult(ResolveInternal(connectionStringName));
        }

        private string ResolveInternal(string connectionStringName)
        {
            //Get module specific value if provided
            if (!connectionStringName.IsNullOrEmpty())
            {
                var moduleConnString = Options.ConnectionStrings.GetOrDefault(connectionStringName);
                if (!moduleConnString.IsNullOrEmpty())
                {
                    return moduleConnString;
                }
            }

            //Get default value
            return Options.ConnectionStrings.Default;
        }
    }
}
