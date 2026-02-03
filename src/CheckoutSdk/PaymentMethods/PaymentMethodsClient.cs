using Checkout.PaymentMethods.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentMethods
{
    public class PaymentMethodsClient : AbstractClient, IPaymentMethodsClient
    {
        private const string PaymentMethodsPath = "payment-methods";

        public PaymentMethodsClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<GetAvailablePaymentMethodsResponse> GetAvailablePaymentMethods(
            string processingChannelId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("processingChannelId", processingChannelId);

            var queryParameters = new Dictionary<string, object>
            {
                { "processing_channel_id", processingChannelId }
            };

            return ApiClient.Get<GetAvailablePaymentMethodsResponse>(
                BuildPathWithQuery(PaymentMethodsPath, queryParameters),
                SdkAuthorization(),
                cancellationToken);
        }

        // Common methods
        private string BuildPathWithQuery(string basePath, IDictionary<string, object> queryParameters)
        {
            if (queryParameters == null || queryParameters.Count == 0)
                return basePath;

            var queryPairs = new List<string>();
            foreach (var kvp in queryParameters)
            {
                queryPairs.Add($"{kvp.Key}={kvp.Value}");
            }

            return $"{basePath}?{string.Join("&", queryPairs)}";
        }
    }
}