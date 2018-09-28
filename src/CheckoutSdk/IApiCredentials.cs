using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout
{
    /// <summary>
    /// Defines an interface for different authorizatiopn/authentication schemes/types.
    /// </summary>
    public interface IApiCredentials
    {
        /// <summary>
        /// Authorizes the request.
        /// </summary>
        /// <param name="httpRequest">The HTTP request to authorize.</param>
        /// <returns>A task that completes when the provided <paramref="httpRequest"/> has been authorized.</returns>
        Task AuthorizeAsync(HttpRequestMessage httpRequest);
    }
}