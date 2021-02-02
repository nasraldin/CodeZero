using System.Threading;
using System.Threading.Tasks;

namespace CodeZero.Data
{
    public interface IAppDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
