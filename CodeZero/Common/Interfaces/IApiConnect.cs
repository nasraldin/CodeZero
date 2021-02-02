using System.Threading.Tasks;

namespace CodeZero.Common.Interfaces
{
    public interface IApiConnect
    {
        Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, TRequest request);
        Task<TResponse> GetDataAsync<TResponse>(string endPoint);
    }
}
