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
        private const string path = "disputes";

        public DisputesClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<GetDisputesResponse> GetDisputesAsync(GetDisputesRequest getDisputesRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (getDisputesRequest == null) throw new ArgumentNullException(nameof(getDisputesRequest));
            var pathWithQuery = getDisputesRequest.PathWithQuery(path);
            
            return _apiClient.GetAsync<GetDisputesResponse>(pathWithQuery, _credentials, cancellationToken);
        }

        public Task<Dispute> GetDisputeAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<Dispute>($"{path}/{id}", _credentials, cancellationToken);
        }

        public Task<Type> AcceptDisputeAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<Type>($"{path}/{id}/accept", _credentials, cancellationToken, null);
        }

        public Task<Type> ProvideDisputeEvidenceAsync(string id, DisputeEvidence disputeEvidence, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PutAsync<Type>($"{path}/{id}/evidence", _credentials, cancellationToken, disputeEvidence);
        }

        public Task<Type> SubmitDisputeEvidenceAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<Type>($"{path}/{id}/evidence", _credentials, cancellationToken, null);
        }

        public Task<DisputeEvidenceResponse> GetDisputeEvidenceAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<DisputeEvidenceResponse>($"{path}/{id}/evidence", _credentials, cancellationToken);
        }
    }
}
