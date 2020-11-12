using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Instruments
{
    /// <summary>
    /// Default implementation of <see cref="IInstrumentsClient"/>.
    /// </summary>
    public class InstrumentsClient : IInstrumentsClient
    {
        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;
        private const string path = "instruments";

        public InstrumentsClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<CreateInstrumentResponse> CreateAnInstrument(InstrumentRequest instrumentRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PostAsync<CreateInstrumentResponse>(path, _credentials, cancellationToken, instrumentRequest);
        }

        public Task<GetInstrumentResponse> GetInstrumentDetails(string instrumentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.GetAsync<GetInstrumentResponse>($"{path}/{instrumentId}", _credentials, cancellationToken);
        }

        public Task<UpdateInstrumentResponse> UpdateInstrumentDetails(string instrumentId, UpdateInstrumentRequest updateInstrumentRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _apiClient.PatchAsync<UpdateInstrumentResponse>($"{path}/{instrumentId}", _credentials, cancellationToken, updateInstrumentRequest);
        }
    }
}
