using Checkout.Common;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    public class ApiClient : IApiClient
    {
        private readonly ILogger _log = LogProvider.GetLogger(typeof(ApiClient));

        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer = new JsonSerializer();

        public ApiClient(IHttpClientFactory httpClientFactory, Uri baseUri)
        {
            CheckoutUtils.ValidateParams("httpClientFactory", httpClientFactory, "baseUri", baseUri);
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = baseUri;
            _httpClient = httpClient;
        }

        public async Task<TResult> Get<TResult>(
            string path,
            SdkAuthorization authorization,
            CancellationToken cancellationToken = default)
        {
            using var httpResponse = await SendRequestAsync(
                HttpMethod.Get,
                path,
                authorization,
                null,
                cancellationToken,
                null
            );
            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Post<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null)
        {
            using var httpResponse = await SendRequestAsync(
                HttpMethod.Post,
                path,
                authorization,
                request,
                cancellationToken,
                idempotencyKey
            );

            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Post<TResult>(string path, SdkAuthorization authorization,
            IDictionary<int, Type> resultTypeMappings,
            object request = null, CancellationToken cancellationToken = default, string idempotencyKey = null)
            where TResult : Resource
        {
            using var httpResponse = await SendRequestAsync(
                HttpMethod.Post,
                path,
                authorization,
                request,
                cancellationToken,
                idempotencyKey
            );

            resultTypeMappings.TryGetValue((int)httpResponse.StatusCode, out var responseType);

            if (responseType == null)
                throw new InvalidOperationException(
                    $"The status code {(int)httpResponse.StatusCode} is not mapped to a result type");

            return await DeserializeJsonAsync(httpResponse, responseType);
        }

        public async Task<TResult> Patch<TResult>(string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null)
        {
            using var httpResponse = await SendRequestAsync(
                new HttpMethod("PATCH"),
                path,
                authorization,
                request,
                cancellationToken,
                null
            );
            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Put<TResult>(string path, SdkAuthorization authorization, object request = null,
            CancellationToken cancellationToken = default, string idempotencyKey = null)
        {
            using var httpResponse = await SendRequestAsync(
                HttpMethod.Put,
                path,
                authorization,
                request,
                cancellationToken,
                null
            );
            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Delete<TResult>(string path,
            SdkAuthorization authorization,
            CancellationToken cancellationToken = default)
        {
            using var httpResponse = await SendRequestAsync(
                HttpMethod.Delete,
                path,
                authorization,
                null,
                cancellationToken,
                null
            );
            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Query<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default)
        {
            var json = _serializer.Serialize(request);
            var dictionary =
                (IDictionary<string, string>)_serializer.Deserialize(json, typeof(IDictionary<string, string>));
            using var httpResponse = await SendRequestAsync(
                HttpMethod.Get,
                QueryHelpers.AddQueryString(path, dictionary),
                authorization,
                null,
                cancellationToken,
                null
            );
            return await DeserializeJsonAsync<TResult>(httpResponse);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(
            HttpMethod httpMethod,
            string path,
            SdkAuthorization authorization,
            object requestBody,
            CancellationToken cancellationToken,
            string idempotencyKey)
        {
            CheckoutUtils.ValidateParams("httpMethod", httpMethod, "path", path, "authorization", authorization);

            HttpContent httpContent = null;
            if (requestBody != null)
            {
                if (requestBody is MultipartFormDataContent content)
                {
                    httpContent = content;
                }
                else
                {
                    httpContent = new StringContent(_serializer.Serialize(requestBody), Encoding.UTF8,
                        MediaTypeNames.Application.Json);
                }
            }

            HttpResponseMessage httpResponse = await Invoke(
                httpMethod,
                path,
                authorization,
                httpContent,
                cancellationToken,
                idempotencyKey);

            await ValidateResponseAsync(httpResponse);

            return httpResponse;
        }

        private async Task<HttpResponseMessage> Invoke(
            HttpMethod httpMethod,
            string path,
            SdkAuthorization authorization,
            HttpContent httpContent,
            CancellationToken cancellationToken,
            string idempotencyKey)
        {
            CheckoutUtils.ValidateParams("httpMethod", httpMethod, "path", path, "authorization", authorization);
            var httpRequest = new HttpRequestMessage(httpMethod, path) {Content = httpContent};
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

        private async Task ValidateResponseAsync(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                return;
            }

            httpResponse.Headers.TryGetValues("Cko-Request-Id", out var requestIdHeader);
            var requestId = requestIdHeader?.FirstOrDefault();
            var json = await httpResponse.Content.ReadAsStringAsync();
            var errorDetails = _serializer.Deserialize(json);
            throw new CheckoutApiException(requestId, httpResponse.StatusCode, errorDetails);
        }

        private async Task<TResult> DeserializeJsonAsync<TResult>(HttpResponseMessage httpResponse)
        {
            var result = await DeserializeJsonAsync(httpResponse, typeof(TResult));
            return (TResult)result;
        }

        private async Task<dynamic> DeserializeJsonAsync(HttpResponseMessage httpResponse, Type resultType)
        {
            if (httpResponse.StatusCode == HttpStatusCode.NoContent ||
                httpResponse.Content.Headers.ContentLength == 0)
            {
                return null;
            }

            var json = await httpResponse.Content.ReadAsStringAsync();
            return _serializer.Deserialize(json, resultType);
        }
    }
}