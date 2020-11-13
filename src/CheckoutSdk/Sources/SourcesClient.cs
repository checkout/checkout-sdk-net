using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sources
{
    /// <summary>
    /// Default implementation of <see cref="ISourcesClient"/>.
    /// </summary>
    public class SourcesClient : ISourcesClient
    {
        private static readonly Dictionary<HttpStatusCode, Type> SourceResponseMappings = new Dictionary<HttpStatusCode, Type>
        {
            { HttpStatusCode.Created, typeof(SourceProcessed)}
        };

        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;
        private readonly ISerializer _serializer;

        public SourcesClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : this(apiClient, configuration, new JsonSerializer())
        {

        }

        public SourcesClient(IApiClient apiClient, CheckoutConfiguration configuration, ISerializer serializer)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);

            _serializer = serializer;
        }

        public Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, SourceResponse Content)> AddAPaymentSource(SourceRequest sourceRequest)
        {
            return RequestSourceAsync(sourceRequest, SourceResponseMappings);
        }

        private async Task<(HttpStatusCode StatusCode, HttpResponseHeaders Headers, SourceResponse Content)> RequestSourceAsync(SourceRequest sourceRequest, Dictionary<HttpStatusCode, Type> resultTypeMappings, CancellationToken cancellationToken = default(CancellationToken))
        {
            const string path = "sources";
            var (StatusCode, Headers, Content) = await _apiClient.PostAsync(path, _credentials, resultTypeMappings, cancellationToken, sourceRequest);
            return (StatusCode, Headers, _serializer.Deserialize(_serializer.Serialize(Content), (Content as object).GetType()));
        }
    }
}
