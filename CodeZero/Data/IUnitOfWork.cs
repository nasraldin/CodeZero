using System.Threading.Tasks;

namespace CodeZero.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}