using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Checkout.Logging;

namespace Checkout
{
    public class ApiClient : IApiClient
    {
        private static readonly ILog Logger = LogProvider.For<ApiClient>();

        private readonly CheckoutConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer;

        public ApiClient(CheckoutConfiguration configuration)
            : this(configuration, new DefaultHttpClientFactory(), new JsonSerializer())
        {

        }

        public ApiClient(CheckoutConfiguration configuration, IHttpClientFactory httpClientFactory, ISerializer serializer)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (httpClientFactory == null) throw new ArgumentNullException(nameof(httpClientFactory));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

            _httpClient = httpClientFactory.Create();
        }

        public async Task<ApiResponse<TResult>> PostAsync<TRequest, TResult>(string path, TRequest request)
        {
            // handle absolute URI
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, GetRequestUri(path));
            httpRequest.Content = new StringContent(_serializer.Serialize(request), Encoding.UTF8, "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue(_configuration.SecretKey);
            httpRequest.Headers.UserAgent.ParseAdd("checkout-sdk-net/1.0.0");

            Logger.Info("{HttpMethod} {Uri}", HttpMethod.Post, httpRequest.RequestUri.AbsoluteUri);
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (httpResponse.StatusCode == (HttpStatusCode)422)
            {
                var errorJson = await httpResponse.Content.ReadAsStringAsync();
                var error = _serializer.Deserialize<Error>(errorJson);

                return new ApiResponse<TResult>
                {
                    StatusCode = httpResponse.StatusCode,
                    Error = error
                };
            }

            httpResponse.EnsureSuccessStatusCode();

            var json = await httpResponse.Content.ReadAsStringAsync();
            var result = _serializer.Deserialize<TResult>(json);

            // TODO error handling

            var apiResponse = new ApiResponse<TResult>
            {
                Result = result,
                StatusCode = httpResponse.StatusCode
            };

            return apiResponse;
        }

        public async Task<ApiResponse<TResult>> PostAsync<TRequest, TResult>(string path, TRequest request, Dictionary<HttpStatusCode, Type> responseTypeMappings)
        {
            // handle absolute URI
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, GetRequestUri(path));
            httpRequest.Content = new StringContent(_serializer.Serialize(request), Encoding.UTF8, "application/json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue(_configuration.SecretKey);
            httpRequest.Headers.UserAgent.ParseAdd("checkout-sdk-net/1.0.0");

            Logger.Info("{HttpMethod} {Uri}", HttpMethod.Post, httpRequest.RequestUri.AbsoluteUri);
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (httpResponse.StatusCode == (HttpStatusCode)422)
            {
                var errorJson = await httpResponse.Content.ReadAsStringAsync();
                var error = _serializer.Deserialize<Error>(errorJson);

                return new ApiResponse<TResult>
                {
                    StatusCode = httpResponse.StatusCode,
                    Error = error
                };
            }

            httpResponse.EnsureSuccessStatusCode();

            var json = await httpResponse.Content.ReadAsStringAsync();

            responseTypeMappings.TryGetValue(httpResponse.StatusCode, out var type);

            var result = _serializer.Deserialize(json, type);

            return new ApiResponse<TResult>
            {
                Result = (TResult)result,
                StatusCode = httpResponse.StatusCode
            };

        }

        private Uri GetRequestUri(string path)
        {
            var baseUri = new Uri(_configuration.Uri);
            Uri.TryCreate(baseUri, path, out var uri);

            return uri;
        }
    }
}