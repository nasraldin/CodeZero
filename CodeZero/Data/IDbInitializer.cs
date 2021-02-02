using System.Threading.Tasks;

namespace CodeZero.Data
{
    public interface IDbInitializer
    {
        Task SeedAsync();
    }
}
