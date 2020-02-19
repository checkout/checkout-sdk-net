using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes
{
    /// <summary>
    /// Default implementation of <see cref="IDisputesClient"/>.
    /// </summary>
    public class DisputesClient : IDisputesClient
    {
        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;

        public DisputesClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<GetDisputesResponse> GetDisputesAsync(GetDisputesRequest getDisputesRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            const string path = "disputes";
            string pathWithQuery = getDisputesRequest.PathWithQuery(path);
            
            return _apiClient.GetAsync<GetDisputesResponse>(pathWithQuery, _credentials, cancellationToken);
        }
    }
}
