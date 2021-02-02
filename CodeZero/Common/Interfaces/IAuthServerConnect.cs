using System.Threading.Tasks;

namespace CodeZero.Common.Interfaces
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
