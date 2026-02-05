using Checkout.PaymentMethods.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentMethods
{
    public interface IPaymentMethodsClient
    {
        /// <summary>
        /// Get a list of all available payment methods for a specific Processing Channel ID.
        /// </summary>
        /// <param name="processingChannelId">The processing channel to be used for payment methods retrieval</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<GetAvailablePaymentMethodsResponse> GetAvailablePaymentMethods(string processingChannelId,
                                                                CancellationToken cancellationToken = default);
    }
}