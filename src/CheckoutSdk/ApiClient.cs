using Checkout.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Logging;

namespace Checkout.Sdk
{
    public class ApiClient : IApiClient
    {
        private const HttpStatusCode Unprocessable = (HttpStatusCode)422;
        private static readonly ILog Logger = LogProvider.For<ApiClient>();

        private readonly CheckoutConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer;

        public ApiClient(CheckoutConfiguration configuration)
            : this(configuration, new DefaultHttpClientFactory(), new JsonSerializer())
        {
        }

        public ApiClient(CheckoutConfiguration configuration, IHttpClientFactory httpClientFactory)
            : this(configuration, httpClientFactory, new JsonSerializer())
        {
        }

        public ApiClient(CheckoutConfiguration configuration, ISerializer serializer)
            : this(configuration, new DefaultHttpClientFactory(), serializer)
        {
        }

        public ApiClient(
            CheckoutConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ISerializer serializer)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (httpClientFactory == null) throw new ArgumentNullException(nameof(httpClientFactory));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<TResult> GetAsync<TResult>(string path, IApiCredentials credentials, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));   

            var httpResponse = await SendRequestAsync(HttpMethod.Get, path, credentials, null, cancellationToken);
            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        public async Task<TResult> PostAsync<TResult>(string path, IApiCredentials credentials, CancellationToken cancellationToken, object request = null)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));

            var httpResponse = await SendRequestAsync(HttpMethod.Post, path, credentials, request, cancellationToken);
            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        public async Task<dynamic> PostAsync(string path, IApiCredentials credentials, object request, Dictionary<HttpStatusCode, Type> resultTypeMappings, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));
            if (resultTypeMappings == null) throw new ArgumentNullException(nameof(resultTypeMappings));

            var httpResponse = await SendRequestAsync(HttpMethod.Post, path, credentials, request, cancellationToken);

            if (!resultTypeMappings.TryGetValue(httpResponse.StatusCode, out Type resultType))
                throw new KeyNotFoundException($"The status code {httpResponse.StatusCode} is not mapped to a result type");

            return await DeserializeJsonAsync(httpResponse, resultType);
        }

        private async Task<TResult> DeserializeJsonAsync<TResult>(HttpResponseMessage httpResponse)
        {
            var result = await DeserializeJsonAsync(httpResponse, typeof(TResult));
            return (TResult)result;
        }

        private async Task<dynamic> DeserializeJsonAsync(HttpResponseMessage httpResponse, Type resultType)
        {
            if (httpResponse.Content == null)
                return null;

            var json = await httpResponse.Content.ReadAsStringAsync();
            return _serializer.Deserialize(json, resultType);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpMethod httpMethod, string path, IApiCredentials credentials, object request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var httpRequest = new HttpRequestMessage(httpMethod, GetRequestUri(path));
            httpRequest.Headers.UserAgent.ParseAdd("checkout-sdk-net/" + ReflectionUtils.GetAssemblyVersion<ApiClient>());

            await credentials.AuthorizeAsync(httpRequest);

            if (request != null)
            {
                httpRequest.Content = new StringContent(_serializer.Serialize(request), Encoding.UTF8, "application/json");
            }

            Logger.Info("{HttpMethod} {Uri}", httpMethod, httpRequest.RequestUri.AbsoluteUri);

            var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
            await ValidateResponseAsync(httpResponse);

            return httpResponse;
        }

        private async Task ValidateResponseAsync(HttpResponseMessage httpResponse)
        {

            if (!httpResponse.IsSuccessStatusCode)
            {
                httpResponse.Headers.TryGetValues("Cko-Request-Id", out var requestIdHeader);
                var requestId = requestIdHeader?.FirstOrDefault();

                if (httpResponse.StatusCode == Unprocessable)
                {
                    var error = await DeserializeJsonAsync<ErrorResponse>(httpResponse);
                    throw new CheckoutValidationException(error, httpResponse.StatusCode, requestId);
                }

                if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                    throw new CheckoutResourceNotFoundException(httpResponse.StatusCode, requestId);

                throw new CheckoutApiException(httpResponse.StatusCode, requestId);
            }
        }

        private Uri GetRequestUri(string path)
        {
            var baseUri = new Uri(_configuration.Uri);
            Uri.TryCreate(baseUri, path, out var uri);

            return uri;
        }
    }
}