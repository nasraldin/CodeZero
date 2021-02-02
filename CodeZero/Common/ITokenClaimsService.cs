using System.Threading.Tasks;

namespace CodeZero.Common
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
