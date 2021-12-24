using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Checkout
{
    public class HttpClientTransport : ITransport
    {
        private readonly HttpClient _httpClient;
        private readonly CheckoutConfiguration _configuration;
        private readonly ILogger _log = LogProvider.GetLogger(typeof(HttpClientTransport));

        public HttpClientTransport(CheckoutConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = GetHttpClient(configuration);
        }

        private static HttpClient GetHttpClient(CheckoutConfiguration configuration)
        {
            var httpClient = configuration.HttpClientFactory.CreateClient();
            CheckoutUtils.ValidateParams("httpClient", httpClient);
            return httpClient;
        }

        public async Task<HttpResponseMessage> Invoke(
            HttpMethod httpMethod,
            string path,
            SdkAuthorization authorization,
            HttpContent httpContent,
            CancellationToken cancellationToken,
            string idempotencyKey)
        {
            CheckoutUtils.ValidateParams("httpMethod", httpMethod, "path", path, "authorization", authorization);
            var httpRequest = new HttpRequestMessage(httpMethod, GetRequestUri(path))
            {
                Content = httpContent
            };
            _log.LogInformation(@"{HttpMethod}: {Path}", httpMethod, path);

            httpRequest.Headers.UserAgent.ParseAdd(
                "checkout-sdk-net/" + CheckoutUtils.GetAssemblyVersion<CheckoutSdk>());
            httpRequest.Headers.TryAddWithoutValidation("Authorization", authorization.GetAuthorizationHeader());

            if (!string.IsNullOrWhiteSpace(idempotencyKey))
            {
                httpRequest.Headers.Add("Cko-Idempotency-Key", idempotencyKey);
            }

            return await _httpClient.SendAsync(httpRequest, cancellationToken);
        }

        private Uri GetRequestUri(string path)
        {
            var baseUri = _configuration.Environment.GetAttribute<EnvironmentAttribute>().ApiUri;
            Uri.TryCreate(baseUri, path, out var uri);
            return uri;
        }
    }
}