#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
using Microsoft.Extensions.Logging;
using System.Web;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    public class ApiClient : IApiClient
    {
#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
        private readonly ILogger _log = LogProvider.GetLogger(typeof(ApiClient));
#endif

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
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                HttpMethod.Get,
                path,
                authorization,
                null,
                cancellationToken,
                null
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Post<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                HttpMethod.Post,
                path,
                authorization,
                request,
                cancellationToken,
                idempotencyKey
            );

            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Post<TResult>(
            string path,
            SdkAuthorization authorization,
            IDictionary<int, Type> resultTypeMappings,
            object request = null, CancellationToken cancellationToken = default, string idempotencyKey = null)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
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

            return await DeserializeResponseAsync(httpResponse, responseType);
        }

        public async Task<TResult> Patch<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                new HttpMethod("PATCH"),
                path,
                authorization,
                request,
                cancellationToken,
                null
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Put<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                HttpMethod.Put,
                path,
                authorization,
                request,
                cancellationToken,
                null
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Delete<TResult>(
            string path,
            SdkAuthorization authorization,
            CancellationToken cancellationToken = default)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                HttpMethod.Delete,
                path,
                authorization,
                null,
                cancellationToken,
                null
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        public async Task<TResult> Query<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default)
            where TResult : HttpMetadata
        {
            if (request != null)
            {
                var json = _serializer.Serialize(request);
                var parameters =
                    (Dictionary<string, string>)_serializer.Deserialize(json, typeof(IDictionary<string, string>));
                if (parameters.Count > 0)
                {
                    var queryString = string.Join("&",
                        parameters.Select(kvp =>
#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
                            $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));
#else
                            $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));
#endif

                            path = $"{path}?{queryString}";
                }
            }

            var httpResponse = await SendRequestAsync(
                HttpMethod.Get,
                path,
                authorization,
                null,
                cancellationToken,
                null
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(
            HttpMethod httpMethod,
            string path,
            SdkAuthorization authorization,
            object requestBody,
            CancellationToken cancellationToken,
            string idempotencyKey)
        {
            CheckoutUtils.ValidateParams("httpMethod", httpMethod, "authorization", authorization);

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
                        "application/json");
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
            CheckoutUtils.ValidateParams("httpMethod", httpMethod, "authorization", authorization);
            var httpRequest = new HttpRequestMessage(httpMethod, path) { Content = httpContent };
#if (NETSTANDARD2_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER)
            _log.LogInformation(@"{HttpMethod}: {Path}", httpMethod, path);
#endif

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

        private static async Task<TResult> GetContent<TResult>(HttpResponseMessage httpResponse)
        {
            var result = (object)await httpResponse.Content.ReadAsStringAsync();
            return (TResult)result;
        }

        private async Task<TResult> DeserializeResponseAsync<TResult>(HttpResponseMessage httpResponse)
        {
            return await DeserializeResponseAsync(httpResponse, typeof(TResult));
        }

        private async Task<dynamic> DeserializeResponseAsync(HttpResponseMessage httpResponse, Type resultType)
        {
            dynamic deserializedObject;
            if (httpResponse.StatusCode == HttpStatusCode.NoContent || httpResponse.Content.Headers.ContentLength == 0)
            {
                deserializedObject = Activator.CreateInstance(resultType);
            }
            else if (resultType == typeof(ContentsResponse))
            {
                deserializedObject = new ContentsResponse { Content = await GetContent<string>(httpResponse) };
            }
            else
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                deserializedObject = _serializer.Deserialize(json, resultType);
            }

            await SetHttpMetadata(httpResponse, deserializedObject);

            return deserializedObject;
        }

        private static async Task SetHttpMetadata(HttpResponseMessage httpResponse, dynamic deserializedObject)
        {
            ((HttpMetadata)deserializedObject).Body = await httpResponse.Content.ReadAsStringAsync();
            ((HttpMetadata)deserializedObject).HttpStatusCode = (int)httpResponse.StatusCode;
            IDictionary<string, string> headers = new Dictionary<string, string>();
            foreach (var header in httpResponse.Headers)
            {
                headers.Add(header.Key, header.Value.First());
            }

            ((HttpMetadata)deserializedObject).ResponseHeaders = headers;
        }
    }
}