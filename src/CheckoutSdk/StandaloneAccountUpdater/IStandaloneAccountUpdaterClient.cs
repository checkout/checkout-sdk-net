using Checkout.StandaloneAccountUpdater.Requests;
using Checkout.StandaloneAccountUpdater.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.StandaloneAccountUpdater
{
    public interface IStandaloneAccountUpdaterClient
    {
        /// <summary>
        /// Retrieve updated card credentials.
        /// The following card schemes are supported: Mastercard, Visa, American Express
        /// </summary>
        /// <param name="request">The card update request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<GetUpdatedCardCredentialsResponse> GetUpdatedCardCredentials(GetUpdatedCardCredentialsRequest request,
                                                                    CancellationToken cancellationToken = default);
    }
}