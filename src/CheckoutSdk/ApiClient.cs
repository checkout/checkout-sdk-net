using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Payments;
using Checkout.Webhooks;

namespace Checkout
{
    public class ApiClient : IApiClient
    {
        public ApiClient(string secretKey)
            : this(new CheckoutConfiguration(secretKey))
        {

        }

        public ApiClient(CheckoutConfiguration configuration)
            : this(configuration, () => new HttpClient(), new JsonNetSerializer())
        {

        }

        // Everything can be overridden
        public ApiClient(CheckoutConfiguration configuration, Func<HttpClient> httpClientFactory, ISerializer serializer)
        {
            Payments = new PaymentsClient(this);
        }

        public IPaymentOperations Payments { get; }

        public IWebhookOperations Webhooks => throw new System.NotImplementedException();

        public Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string path, TRequest request)
        {
            return Task.FromResult(new ApiResponse<TResponse>());
        }
    }
}