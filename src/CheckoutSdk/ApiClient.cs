#if NET5_0_OR_GREATER || NETSTANDARD2_0
using Microsoft.Extensions.Logging;
using System.Web;
#endif
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    /// <summary>
    /// Sends HTTP requests to the Checkout.com API and deserializes the responses.
    /// </summary>
    public class ApiClient : IApiClient
    {
#if NET5_0_OR_GREATER || NETSTANDARD2_0
        private readonly ILogger _log = LogProvider.GetLogger(typeof(ApiClient));
#endif

        private const string sdkTelemetryHeader = "cko-sdk-telemetry";
        private const int maxCountInTelemetryQueue = 10;
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUri;
        private readonly ISerializer _serializer = new JsonSerializer();
        private readonly ConcurrentQueue<RequestMetrics> requestMetricsQueue = new ConcurrentQueue<RequestMetrics>();
        private readonly bool _enableTelemetry;

        /// <summary>
        /// Creates a client that sends requests relative to the given base URI.
        /// </summary>
        /// <param name="httpClientFactory">The factory that creates the HTTP client.</param>
        /// <param name="baseUri">The base URI of the API; must use https, http is accepted for loopback hosts.</param>
        /// <param name="enableTelemetry">When true, a cko-sdk-telemetry header with the duration of the previous request is sent.</param>
        /// <exception cref="CheckoutArgumentException">A parameter is null, or baseUri is not absolute or does not use https on a host that is not loopback.</exception>
        public ApiClient(IHttpClientFactory httpClientFactory, Uri baseUri, bool enableTelemetry)
        {
            CheckoutUtils.ValidateParams("httpClientFactory", httpClientFactory, "baseUri", baseUri);
            CheckoutUtils.ValidateHttpsUri("baseUri", baseUri);
            var httpClient = httpClientFactory.CreateClient();
            _baseUri = baseUri;
            _httpClient = httpClient;
            _enableTelemetry = enableTelemetry;
        }

        /// <summary>
        /// Sends a GET request.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The deserialized response, with its HTTP status code, headers and body set.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, or the response body cannot be read as the requested type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
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

        /// <summary>
        /// Sends a GET request with additional headers.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="headers">An object whose public properties are sent as request headers; properties with an empty value are skipped.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The deserialized response, with its HTTP status code, headers and body set.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, or the response body cannot be read as the requested type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
        public async Task<TResult> Get<TResult>(
            string path,
            SdkAuthorization authorization,
            IHeaders headers,
            CancellationToken cancellationToken = default)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                HttpMethod.Get,
                path,
                authorization,
                null,
                cancellationToken,
                null,
                headers
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        /// <summary>
        /// Sends a POST request.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="request">The request body, serialized to JSON unless it is multipart or form url encoded content; may be null.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <param name="idempotencyKey">The value sent in the Cko-Idempotency-Key header; not sent when null or blank.</param>
        /// <returns>The deserialized response, with its HTTP status code, headers and body set.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, or the response body cannot be read as the requested type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
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

        /// <summary>
        /// Sends a POST request and deserializes the response into the type mapped to its status code.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="resultTypeMappings">The response type to use for each HTTP status code.</param>
        /// <param name="request">The request body, serialized to JSON unless it is multipart or form url encoded content; may be null.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <param name="idempotencyKey">The value sent in the Cko-Idempotency-Key header; not sent when null or blank.</param>
        /// <returns>The deserialized response, of the type mapped to the status code.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, is not in resultTypeMappings, or the response body cannot be read as the mapped type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
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
            {
                httpResponse.Headers.TryGetValues("Cko-Request-Id", out var requestIdHeader);
                throw new CheckoutApiException(requestIdHeader?.FirstOrDefault(), httpResponse.StatusCode,
                    new Dictionary<string, object>(),
                    $"The status code {(int)httpResponse.StatusCode} is not mapped to a result type", null);
            }

            return await DeserializeResponseAsync(httpResponse, responseType);
        }

        /// <summary>
        /// Sends a POST request with additional headers.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="request">The request body, serialized to JSON unless it is multipart or form url encoded content; may be null.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <param name="idempotencyKey">The value sent in the Cko-Idempotency-Key header; not sent when null or blank.</param>
        /// <param name="headers">An object whose public properties are sent as request headers; properties with an empty value are skipped.</param>
        /// <returns>The deserialized response, with its HTTP status code, headers and body set.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, or the response body cannot be read as the requested type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
        public async Task<TResult> Post<TResult>(
            string path,
            SdkAuthorization authorization,
            object request,
            CancellationToken cancellationToken,
            string idempotencyKey,
            IHeaders headers)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                HttpMethod.Post,
                path,
                authorization,
                request,
                cancellationToken,
                idempotencyKey,
                headers
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        /// <summary>
        /// Sends a PATCH request.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="request">The request body, serialized to JSON unless it is multipart or form url encoded content; may be null.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <param name="idempotencyKey">The value sent in the Cko-Idempotency-Key header; not sent when null or blank.</param>
        /// <param name="headers">An object whose public properties are sent as request headers; properties with an empty value are skipped. May be null.</param>
        /// <returns>The deserialized response, with its HTTP status code, headers and body set.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, or the response body cannot be read as the requested type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
        public async Task<TResult> Patch<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null,
            IHeaders headers = null)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                new HttpMethod("PATCH"),
                path,
                authorization,
                request,
                cancellationToken,
                idempotencyKey,
                headers
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        /// <summary>
        /// Sends a PUT request.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="request">The request body, serialized to JSON unless it is multipart or form url encoded content; may be null.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <param name="idempotencyKey">Accepted for signature compatibility; it is not sent with PUT requests.</param>
        /// <param name="headers">An object whose public properties are sent as request headers; properties with an empty value are skipped. May be null.</param>
        /// <returns>The deserialized response, with its HTTP status code, headers and body set.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, or the response body cannot be read as the requested type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
        public async Task<TResult> Put<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null,
            IHeaders headers = null)
            where TResult : HttpMetadata
        {
            var httpResponse = await SendRequestAsync(
                HttpMethod.Put,
                path,
                authorization,
                request,
                cancellationToken,
                null,
                headers
            );
            return await DeserializeResponseAsync<TResult>(httpResponse);
        }

        /// <summary>
        /// Sends a DELETE request.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The deserialized response, with its HTTP status code, headers and body set.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, or the response body cannot be read as the requested type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
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

        /// <summary>
        /// Sends a GET request with the properties of the request object as url encoded query parameters.
        /// </summary>
        /// <typeparam name="TResult">The response type.</typeparam>
        /// <param name="path">The request path, relative to the base URI.</param>
        /// <param name="authorization">The authorization used to build the Authorization header.</param>
        /// <param name="request">The object whose serialized properties form the query string; may be null.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The deserialized response, with its HTTP status code, headers and body set.</returns>
        /// <exception cref="CheckoutApiException">The response status code does not indicate success, or the response body cannot be read as the requested type.</exception>
        /// <exception cref="CheckoutArgumentException">The authorization header value is empty or contains characters outside visible ASCII and space.</exception>
        /// <exception cref="CheckoutAuthorizationException">The authorization cannot produce a header for its platform type.</exception>
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
#if NET5_0_OR_GREATER || NETSTANDARD2_0
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
            string idempotencyKey,
            IHeaders headers = null)
        {
            CheckoutUtils.ValidateParams("httpMethod", httpMethod, "authorization", authorization);

            HttpContent httpContent = null;
            if (requestBody != null)
            {
                if (requestBody is MultipartFormDataContent content)
                {
                    httpContent = content;
                }
                else if (requestBody is FormUrlEncodedContent formContent)
                {
                    httpContent = formContent;
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
                idempotencyKey,
                headers);

            await ValidateResponseAsync(httpResponse);

            return httpResponse;
        }

        private async Task<HttpResponseMessage> Invoke(
            HttpMethod httpMethod,
            string path,
            SdkAuthorization authorization,
            HttpContent httpContent,
            CancellationToken cancellationToken,
            string idempotencyKey,
            IHeaders headers = null)
        {
            CheckoutUtils.ValidateParams("httpMethod", httpMethod, "authorization", authorization);

            var pathUri = new Uri(_baseUri, path);
            var httpRequest = new HttpRequestMessage(httpMethod, pathUri) { Content = httpContent };
#if NET5_0_OR_GREATER || NETSTANDARD2_0
            _log.LogInformation(@"{HttpMethod}: {Path}", httpMethod, LoggablePath(path));
#endif

            httpRequest.Headers.UserAgent.ParseAdd(
                "checkout-sdk-net/" + CheckoutUtils.GetAssemblyVersion<CheckoutSdk>());
            var authorizationHeader = authorization.GetAuthorizationHeader();
            ValidateAuthorizationHeader(authorizationHeader);
            httpRequest.Headers.TryAddWithoutValidation("Authorization", authorizationHeader);

            if (!string.IsNullOrWhiteSpace(idempotencyKey))
            {
                httpRequest.Headers.Add("Cko-Idempotency-Key", idempotencyKey);
            }

            if (headers != null)
            {
                foreach (var header in headers.GetType().GetProperties())
                {
                    var value = ResolveHeaderValue(header.GetValue(headers));
                    if (!string.IsNullOrEmpty(value))
                    {
                        var headerName = GetJsonPropertyName(header) ?? header.Name;
                        httpRequest.Headers.Add(headerName, value);
                    }
                }
            }

            if (_enableTelemetry)
            {
                var currentRequestId = Guid.NewGuid().ToString();
                if (requestMetricsQueue.TryDequeue(out var lastRequestMetric))
                {
                    lastRequestMetric.RequestId = currentRequestId;
                    httpRequest.Headers.TryAddWithoutValidation(sdkTelemetryHeader, _serializer.Serialize(lastRequestMetric));
                }

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var result = await _httpClient.SendAsync(httpRequest, cancellationToken);
                stopwatch.Stop();

                if (requestMetricsQueue.Count < maxCountInTelemetryQueue)
                {
                    lastRequestMetric.PrevRequestDuration = stopwatch.ElapsedMilliseconds;
                    requestMetricsQueue.Enqueue(new RequestMetrics()
                    {
                        PrevRequestDuration = stopwatch.ElapsedMilliseconds,
                        PrevRequestId = currentRequestId
                    });
                }

                return result;
            }
            else
            {
                return await _httpClient.SendAsync(httpRequest, cancellationToken);
            }
        }

        private async Task ValidateResponseAsync(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                return;
            }

            httpResponse.Headers.TryGetValues("Cko-Request-Id", out var requestIdHeader);
            var requestId = requestIdHeader?.FirstOrDefault();
            // Content can be null on net45 and netstandard2.0.
            var json = httpResponse.Content == null ? string.Empty : await httpResponse.Content.ReadAsStringAsync();
            var errorDetails = GetErrorDetails(json);

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
            dynamic deserializedObject = null;

            if (httpResponse.StatusCode == HttpStatusCode.NoContent ||
                httpResponse.Content == null ||
                (httpResponse.Content.Headers.ContentLength.HasValue && httpResponse.Content.Headers.ContentLength.Value == 0))
            {
                try
                {
                    deserializedObject = Activator.CreateInstance(resultType) ?? new DefaultHttpMetadata();
                }
                catch (Exception e)
                {
                    httpResponse.Headers.TryGetValues("Cko-Request-Id", out var requestIdHeader);
                    throw new CheckoutApiException(requestIdHeader?.FirstOrDefault(), httpResponse.StatusCode,
                        new Dictionary<string, object>(), "The API response could not be read.", e);
                }
            }
            else if (resultType == typeof(ContentsResponse))
            {
                deserializedObject = new ContentsResponse { Content = await GetContent<string>(httpResponse) };
            }
            else
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                try
                {
                    deserializedObject = _serializer.Deserialize(json, resultType);

                    // A JSON null body would otherwise reach the caller as null.
                    if (deserializedObject == null)
                    {
                        deserializedObject = Activator.CreateInstance(resultType) ?? new DefaultHttpMetadata();
                    }
                }
                catch (Exception e) when (!(e is CheckoutException))
                {
                    // Not only JsonException: the polymorphic converters throw their own
                    // exceptions (InvalidCastException, bare Exception) on unexpected payloads.
                    httpResponse.Headers.TryGetValues("Cko-Request-Id", out var requestIdHeader);
                    throw new CheckoutApiException(requestIdHeader?.FirstOrDefault(), httpResponse.StatusCode,
                        GetErrorDetails(json), "The API response body could not be read.", e);
                }
            }

            if (deserializedObject is HttpMetadata metadata)
            {
                metadata.Body = metadata.Body ?? string.Empty;
                metadata.HttpStatusCode = metadata.HttpStatusCode == null ? 0 : (int)httpResponse.StatusCode;
                metadata.ResponseHeaders = metadata.ResponseHeaders ?? new Dictionary<string, string>();
            }

            if (deserializedObject != null)
            {
                await SetHttpMetadata(httpResponse, deserializedObject);
            }

            return deserializedObject;
        }


        private static async Task SetHttpMetadata(HttpResponseMessage httpResponse, dynamic deserializedObject)
        {
            if (deserializedObject is HttpMetadata metadata)
            {
                metadata.Body = await (httpResponse.Content?.ReadAsStringAsync() ?? Task.FromResult(string.Empty));
                metadata.HttpStatusCode = (int)httpResponse.StatusCode;
                metadata.ResponseHeaders = httpResponse.Headers?.ToDictionary(
                    h => h.Key,
                    h => h.Value.FirstOrDefault() ?? string.Empty
                ) ?? new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// The request path as it is logged: the query string is left out and every segment that holds an email
        /// address is replaced with <c>***</c>.
        /// </summary>
        /// <param name="path">The relative request path, optionally with a query string.</param>
        /// <returns>The path to log.</returns>
        internal static string LoggablePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            var queryStart = path.IndexOf('?');
            var segments = (queryStart < 0 ? path : path.Substring(0, queryStart)).Split('/');
            for (var i = 0; i < segments.Length; i++)
            {
                if (segments[i].IndexOf('@') >= 0 ||
                    segments[i].IndexOf("%40", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    segments[i] = "***";
                }
            }

            return string.Join("/", segments);
        }

        private static string ResolveHeaderValue(object raw)
        {
            if (raw == null) return null;

            var type = raw.GetType();
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (type.IsEnum)
            {
                var member = type.GetMember(raw.ToString()).FirstOrDefault();
                var attr = member?.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                    .Cast<EnumMemberAttribute>().FirstOrDefault();
                if (attr?.Value != null) return attr.Value;
            }

            // bool.ToString() returns "True"/"False", capitalised. Header values are not JSON, so
            // nothing downstream lower-cases them: a boolean header would reach the wire as
            // "True". HTTP APIs spell boolean header values "true"/"false", and a strict
            // server-side parser is entitled to read "True" as anything but true.
            if (type == typeof(bool))
            {
                return (bool)raw ? "true" : "false";
            }

            return raw.ToString();
        }

        private static string GetJsonPropertyName(System.Reflection.PropertyInfo property)
        {
            // Check for System.Text.Json JsonPropertyName attribute
            var jsonPropertyNameAttr = property.GetCustomAttributes(false)
                .FirstOrDefault(attr => attr.GetType().Name == "JsonPropertyNameAttribute");
            if (jsonPropertyNameAttr != null)
            {
                var nameProperty = jsonPropertyNameAttr.GetType().GetProperty("Name");
                if (nameProperty != null)
                {
                    return nameProperty.GetValue(jsonPropertyNameAttr)?.ToString();
                }
            }

            // Check for Newtonsoft.Json JsonProperty attribute
            var jsonPropertyAttr = property.GetCustomAttributes(false)
                .FirstOrDefault(attr => attr.GetType().Name == "JsonPropertyAttribute");
            if (jsonPropertyAttr != null)
            {
                var propertyNameProperty = jsonPropertyAttr.GetType().GetProperty("PropertyName");
                if (propertyNameProperty != null)
                {
                    return propertyNameProperty.GetValue(jsonPropertyAttr)?.ToString();
                }
            }

            return null;
        }

        private IDictionary<string, object> GetErrorDetails(string json)
        {
            IDictionary<string, object> errorDetails;
            try
            {
                errorDetails = _serializer.Deserialize(json);
            }
            catch
            {
                errorDetails = new Dictionary<string, object>
                {
                    { "error message", json }
                };
            }

            return errorDetails ?? new Dictionary<string, object>();
        }

        // TryAddWithoutValidation does not check the value, and a CR or LF in it would split the
        // request. Only visible ASCII and the space of "Bearer <token>" are allowed. The message
        // never echoes the credential.
        private static void ValidateAuthorizationHeader(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new CheckoutArgumentException("Authorization header value cannot be empty");
            }

            foreach (var c in value)
            {
                if (c < 0x20 || c > 0x7E)
                {
                    throw new CheckoutArgumentException(
                        "Authorization header value contains characters that are not allowed");
                }
            }
        }
    }
}