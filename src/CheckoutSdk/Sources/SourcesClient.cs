using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sources
{
    /// <summary>
    /// Default implementation of <see cref="ISourcesClient"/>.
    /// </summary>
    public class SourcesClient : ISourcesClient
    {
        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;

        public SourcesClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<SourceResponse> RequestAsync(SourceRequest sourceRequest)
        {
            return RequestSourceAsync(sourceRequest);
        }

        private async Task<SourceResponse> RequestSourceAsync(SourceRequest sourceRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var apiResponse = await _apiClient.PostAsync<SourceResponse>("sources", _credentials, cancellationToken, sourceRequest);
            return apiResponse;
        }
    }
}