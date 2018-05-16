using System.Threading.Tasks;

namespace Checkout
{
    public interface IApiClient
    {
        Task<ApiResponse<TResult>> PostAsync<TRequest, TResult>(string uri, TRequest request);
    }
}