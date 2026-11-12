using Checkout.Authentication;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Shouldly;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout
{
    [Collection("NonParallel")]
    public class CredentialsAndResponseHandlingTests : UnitTestFixture, IDisposable
    {
        private readonly List<HttpClient> _httpClients = new List<HttpClient>();

        public void Dispose()
        {
            _httpClients.ForEach(c => c.Dispose());
        }

        private HttpClient Track(HttpClient httpClient)
        {
            _httpClients.Add(httpClient);
            return httpClient;
        }

        private const string ValidTokenBody =
            "{\"access_token\": \"valid_token\", \"token_type\": \"Bearer\", \"expires_in\": 3600}";

        private static Mock<IHttpClientFactory> FactoryFor(HttpClient httpClient)
        {
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(_ => _.CreateClient()).Returns(httpClient);
            return factory;
        }

        private static Mock<HttpMessageHandler> HandlerReturning(Func<HttpResponseMessage> response,
            List<HttpRequestMessage> sent = null, TimeSpan? delay = null)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns<HttpRequestMessage, CancellationToken>(async (request, _) =>
                {
                    if (sent != null)
                    {
                        lock (sent)
                        {
                            sent.Add(request);
                        }
                    }

                    if (delay.HasValue)
                    {
                        await Task.Delay(delay.Value);
                    }

                    return response();
                });
            return handler;
        }

        private static HttpResponseMessage Json(HttpStatusCode status, string body, string mediaType = "application/json")
        {
            return new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, mediaType) };
        }

        private static OAuthSdkCredentials OAuthCredentials(HttpClient httpClient)
        {
            return new OAuthSdkCredentials(FactoryFor(httpClient).Object, new Uri("https://fake-auth.com/connect/token"),
                "test_client_id", "test_client_secret", new HashSet<OAuthScope>());
        }

        // OAuth token request

        [Fact]
        public async Task ShouldUseSharedHttpClientThatAlreadySentRequestsWithoutChangingIt()
        {
            var sent = new List<HttpRequestMessage>();
            using var httpClient = new HttpClient(HandlerReturning(() => Json(HttpStatusCode.OK, ValidTokenBody), sent).Object);
            await httpClient.GetAsync("https://merchant.example.com/own-call");

            var credentials = OAuthCredentials(httpClient);
            credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth).GetAuthorizationHeader()
                .ShouldBe("Bearer valid_token");

            httpClient.BaseAddress.ShouldBeNull();
            sent.Last().RequestUri.ShouldBe(new Uri("https://fake-auth.com/connect/token"));
        }

        [Fact]
        public void ShouldRejectTokenResponseWithErrorStatusEvenIfBodyLooksValid()
        {
            using var httpClient = new HttpClient(HandlerReturning(() => Json(HttpStatusCode.InternalServerError, ValidTokenBody)).Object);

            var ex = Should.Throw<CheckoutAuthorizationException>(() => OAuthCredentials(httpClient).InitAccess());
            ex.Message.ShouldNotContain("test_client_secret");
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized, "{\"error\": \"invalid_client\"}")]
        [InlineData(HttpStatusCode.OK, "null")]
        [InlineData(HttpStatusCode.OK, "{\"access_token\": \"t\", \"token_type\": \"Bearer\", \"expires_in\": 9223372036854775807}")]
        public void ShouldKeepTheTokenFailureMessage(HttpStatusCode status, string body)
        {
            using var httpClient = new HttpClient(HandlerReturning(() => Json(status, body)).Object);

            var ex = Should.Throw<CheckoutAuthorizationException>(() => OAuthCredentials(httpClient).InitAccess());
            ex.Message.ShouldBe("OAuth client_credentials authentication failed with error: invalid_client");
        }

        [Fact]
        public void ShouldRejectJsonNullTokenBody()
        {
            using var httpClient = new HttpClient(HandlerReturning(() => Json(HttpStatusCode.OK, "null")).Object);

            Should.Throw<CheckoutAuthorizationException>(() => OAuthCredentials(httpClient).InitAccess());
        }

        [Fact]
        public async Task ShouldRequestTheTokenOnceUnderConcurrentFirstUse()
        {
            var sent = new List<HttpRequestMessage>();
            using var httpClient = new HttpClient(HandlerReturning(() => Json(HttpStatusCode.OK, ValidTokenBody), sent,
                TimeSpan.FromMilliseconds(200)).Object);
            var credentials = OAuthCredentials(httpClient);

            var calls = Enumerable.Range(0, 8).Select(_ => Task.Run(() =>
                credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth).GetAuthorizationHeader()));
            var headers = await Task.WhenAll(calls);

            headers.ShouldAllBe(h => h == "Bearer valid_token");
            sent.Count.ShouldBe(1);
        }

        [Theory]
        [InlineData("300000000000")]
        [InlineData("9223372036854775807")]
        public void ShouldRejectOutOfRangeTokenExpiry(string expiresIn)
        {
            var body = "{\"access_token\": \"valid_token\", \"token_type\": \"Bearer\", \"expires_in\": " + expiresIn + "}";
            using var httpClient = new HttpClient(HandlerReturning(() => Json(HttpStatusCode.OK, body)).Object);

            Should.Throw<CheckoutAuthorizationException>(() => OAuthCredentials(httpClient).InitAccess());
        }

        [Fact]
        public void ShouldRejectNonHttpsApiBaseUri()
        {
            Should.Throw<CheckoutArgumentException>(() =>
                new ApiClient(FactoryFor(Track(new HttpClient())).Object, new Uri("http://api.example.com"), false));
        }

        [Fact]
        public void ShouldAllowHttpApiBaseUriOnLoopback()
        {
            Should.NotThrow(() =>
                new ApiClient(FactoryFor(Track(new HttpClient())).Object, new Uri("http://localhost:8080"), false));
        }

        [Fact]
        public void ShouldRejectNonHttpsAuthorizationUri()
        {
            Should.Throw<CheckoutArgumentException>(() =>
                CheckoutSdk.Builder().OAuth().AuthorizationUri(new Uri("http://auth.example.com/connect/token")));
        }

        [Fact]
        public void ShouldAllowHttpAuthorizationUriOnLoopback()
        {
            Should.NotThrow(() =>
                CheckoutSdk.Builder().OAuth().AuthorizationUri(new Uri("http://localhost:8080/connect/token")));
        }

        // Response handling

        private ApiClient ApiClientFor(Mock<HttpMessageHandler> handler)
        {
            return new ApiClient(FactoryFor(Track(new HttpClient(handler.Object))).Object, new Uri("https://api.example.com"), false);
        }

        [Fact]
        public async Task ShouldReturnEmptyInstanceForJsonNullBody()
        {
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "null")));

            var result = await apiClient.Get<FakeResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk));

            result.ShouldNotBeNull();
            result.HttpStatusCode.ShouldBe(200);
        }

        [Fact]
        public async Task ShouldWrapNonJsonSuccessBodyInCheckoutException()
        {
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "<html>gateway</html>", "text/html")));

            var ex = await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Get<FakeResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));
            ex.HttpStatusCode.ShouldBe(HttpStatusCode.OK);
            ex.InnerException.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldGiveNonNullErrorDetailsForJsonNullErrorBody()
        {
            var apiClient = ApiClientFor(HandlerReturning(() => Json((HttpStatusCode)422, "null")));

            var ex = await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Get<FakeResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));
            ex.ErrorDetails.ShouldNotBeNull();
        }

        [Fact]
        public async Task ShouldWrapConverterExceptionInCheckoutApiException()
        {
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "{\"type\":{}}")));

            var ex = await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Get<ThrowingConverterResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));
            ex.HttpStatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldGiveTypedErrorForJsonNullWhenResultTypeCannotBeCreated()
        {
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "null")));

            await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Get<NoDefaultConstructorResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));
        }

        [Fact]
        public async Task ShouldGiveTypedErrorForNoContentWhenResultTypeCannotBeCreated()
        {
            var apiClient = ApiClientFor(HandlerReturning(() => new HttpResponseMessage(HttpStatusCode.NoContent)));

            await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Get<NoDefaultConstructorResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));
        }

        [Fact]
        public async Task ShouldGiveTypedErrorForSuccessStatusWithoutMappedResultType()
        {
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.Accepted, "{}")));

            var ex = await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Post<FakeResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk),
                    new Dictionary<int, Type> { { 201, typeof(FakeResponse) } }));
            ex.HttpStatusCode.ShouldBe(HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task ShouldGiveTypedErrorForErrorResponseWithoutContent()
        {
            var apiClient = ApiClientFor(HandlerReturning(() =>
                new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = null }));

            var ex = await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Get<FakeResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));
            ex.ErrorDetails.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("x\r\nInjected: y")]
        [InlineData("x\nInjected: y")]
        [InlineData("x\0y")]
        [InlineData("xéy")]
        [InlineData("x\ty")]
        [InlineData("x\u007Fy")]
        [InlineData("x\u0085y")]
        [InlineData("x\u2028y")]
        [InlineData("x\uFF41y")]
        [InlineData("Bearer\r\nInjected: y")]
        public async Task ShouldRejectAuthorizationWithControlOrNonAsciiCharactersBeforeSending(string credential)
        {
            var sent = new List<HttpRequestMessage>();
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "{}"), sent));

            var ex = await Should.ThrowAsync<CheckoutArgumentException>(() =>
                apiClient.Get<FakeResponse>("/test", new SdkAuthorization(PlatformType.Previous, credential)));

            ex.Message.ShouldNotContain(credential);
            sent.ShouldBeEmpty();
        }

        [Fact]
        public async Task ShouldLogEscapedPathWithoutQuery()
        {
            var logger = new CapturingLoggerFactory();
            LogProvider.SetLogFactory(logger);
            try
            {
                var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "{}")));

                await apiClient.Get<FakeResponse>("payments/pay_1\r\nFAKE LOG LINE?email=a@b.com",
                    new SdkAuthorization(PlatformType.Default, ValidDefaultSk));

                var line = logger.Messages.ShouldHaveSingleItem();
                line.ShouldNotContain("\r");
                line.ShouldNotContain("\n");
                line.ShouldNotContain("email");
            }
            finally
            {
                LogProvider.SetLogFactory(TestLoggerFactoryHelper.Instance);
            }
        }

        // Secrets in success bodies never reach the exception

        private const string SecretValue = "sek_TopSecretValue123";

        [Theory]
        [InlineData("not json " + SecretValue)]
        [InlineData("{\"session_secret\": \"" + SecretValue + "\", \"type\": {}}")]
        public async Task ShouldKeepSuccessBodySecretsOutOfTheException(string body)
        {
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.Created, body)));

            var ex = await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Get<ThrowingConverterResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));

            ex.ErrorDetails.ShouldBeEmpty();
            ex.InnerException.ShouldBeNull();
            ex.ToString().ShouldNotContain(SecretValue);
        }

        // Custom platform type

        [Theory]
        [InlineData("a,b")]
        [InlineData("a b")]
        [InlineData("x\r\nInjected: y")]
        [InlineData("")]
        public void ShouldRejectCustomCredentialOutsideTokenCharacters(string credential)
        {
            Should.Throw<CheckoutArgumentException>(() => new SdkAuthorization(PlatformType.Custom, credential));
        }

        [Fact]
        public void ShouldEnforceTheTokenLengthLimitOnCustomCredential()
        {
            Should.NotThrow(() => new SdkAuthorization(PlatformType.Custom, new string('a', 4096)));
            Should.Throw<CheckoutArgumentException>(() => new SdkAuthorization(PlatformType.Custom, new string('a', 4097)));
        }

        [Fact]
        public void ShouldAcceptSessionSecretAtTheLengthLimit()
        {
            new SessionSecretSdkCredentials(new string('a', 4096)).Secret.Length.ShouldBe(4096);
        }

        // Other outgoing headers

        private sealed class TestHeaders : IHeaders
        {
            [Newtonsoft.Json.JsonProperty(PropertyName = "X-Test")]
            public string Value { get; set; }
        }

        [Theory]
        [InlineData("key\r\nInjected: y")]
        [InlineData("key\r\n y")]
        [InlineData("keyé")]
        public async Task ShouldRejectIdempotencyKeyWithControlOrNonAsciiCharacters(string key)
        {
            var sent = new List<HttpRequestMessage>();
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "{}"), sent));

            var ex = await Should.ThrowAsync<CheckoutArgumentException>(() =>
                apiClient.Post<FakeResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk),
                    null, CancellationToken.None, key));

            ex.Message.ShouldNotContain(key);
            sent.ShouldBeEmpty();
        }

        [Theory]
        [InlineData("v\r\nInjected: y")]
        [InlineData("v\r\n y")]
        public async Task ShouldRejectCustomHeaderWithControlCharacters(string value)
        {
            var sent = new List<HttpRequestMessage>();
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "{}"), sent));

            await Should.ThrowAsync<CheckoutArgumentException>(() =>
                apiClient.Post<FakeResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk),
                    null, CancellationToken.None, null, new TestHeaders { Value = value }));

            sent.ShouldBeEmpty();
        }

        // Final URI stays on the base host

        [Theory]
        [InlineData("https://evil.example/p")]
        [InlineData("//evil.example/p")]
        [InlineData("http://api.example.com/p")]
        [InlineData("https://api.example.com:8443/p")]
        public async Task ShouldRejectPathThatLeavesTheBaseHost(string path)
        {
            var sent = new List<HttpRequestMessage>();
            var apiClient = ApiClientFor(HandlerReturning(() => Json(HttpStatusCode.OK, "{}"), sent));

            await Should.ThrowAsync<CheckoutArgumentException>(() =>
                apiClient.Get<FakeResponse>(path, new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));

            sent.ShouldBeEmpty();
        }

        // Base URI look-alikes

        [Theory]
        [InlineData("http://localhost.evil.com")]
        [InlineData("http://127.0.0.1.nip.io")]
        [InlineData("http://0.0.0.0")]
        [InlineData("ftp://api.example.com")]
        [InlineData("file:///tmp/x")]
        public void ShouldRejectLookAlikeLoopbackAndOtherSchemes(string uri)
        {
            Should.Throw<CheckoutArgumentException>(() =>
                new ApiClient(FactoryFor(Track(new HttpClient())).Object, new Uri(uri), false));
        }

        [Theory]
        [InlineData("http://[::1]:8080")]
        [InlineData("http://127.1:8080")]
        public void ShouldAllowRealLoopback(string uri)
        {
            Should.NotThrow(() =>
                new ApiClient(FactoryFor(Track(new HttpClient())).Object, new Uri(uri), false));
        }

        // OAuth token that cannot be sent

        [Fact]
        public void ShouldNotCacheTokenThatCannotBeSentInAHeader()
        {
            var sent = new List<HttpRequestMessage>();
            var body = "{\"access_token\": \"bad\\r\\ntoken\", \"token_type\": \"Bearer\", \"expires_in\": 3600}";
            using var httpClient = new HttpClient(HandlerReturning(() => Json(HttpStatusCode.OK, body), sent).Object);
            var credentials = OAuthCredentials(httpClient);

            Should.Throw<CheckoutAuthorizationException>(() => credentials.InitAccess());
            Should.Throw<CheckoutAuthorizationException>(() => credentials.InitAccess());
            sent.Count.ShouldBe(2);
        }

        [Fact]
        public void ShouldKeepTokenBodyOutOfTheTokenException()
        {
            var body = "{\"access_token\": \"" + SecretValue + "\", \"token_type\": \"Bearer\", \"expires_in\": \"x\"}";
            using var httpClient = new HttpClient(HandlerReturning(() => Json(HttpStatusCode.OK, body)).Object);

            var ex = Should.Throw<CheckoutAuthorizationException>(() => OAuthCredentials(httpClient).InitAccess());
            ex.ToString().ShouldNotContain(SecretValue);
        }

        // Static keys

        [Fact]
        public void ShouldRejectSecretKeyWithTrailingNewline()
        {
            const string key = "sk_sbox_abcdefghijklmnopqrstuvwxyz2";
            Should.NotThrow(() => CheckoutSdk.Builder().StaticKeys().SecretKey(key).Environment(Environment.Sandbox).EnvironmentSubdomain("1234doma").Build());
            Should.Throw<CheckoutArgumentException>(() =>
                CheckoutSdk.Builder().StaticKeys().SecretKey(key + "\n").Environment(Environment.Sandbox).EnvironmentSubdomain("1234doma").Build());
        }

        // Token request redirects

        [Fact]
        public void ShouldNotFollowRedirectsOnTheTokenRequestWithTheDefaultFactory()
        {
            var credentials = new OAuthSdkCredentials(new DefaultHttpClientFactory(),
                new Uri("https://fake-auth.com/connect/token"), "id", "secret", new HashSet<OAuthScope>());
            var client = (HttpClient)typeof(OAuthSdkCredentials)
                .GetField("_httpClient", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(credentials);
            var handler = (HttpClientHandler)typeof(HttpMessageInvoker)
                .GetField("_handler", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(client);
            handler.AllowAutoRedirect.ShouldBeFalse();
        }

        [Fact]
        public void ShouldRefuseTokenServedFromAnotherHost()
        {
            using var httpClient = new HttpClient(HandlerReturning(() =>
            {
                var response = Json(HttpStatusCode.OK, ValidTokenBody);
                response.RequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://evil.example/token");
                return response;
            }).Object);

            Should.Throw<CheckoutAuthorizationException>(() => OAuthCredentials(httpClient).InitAccess());
        }

        // Unreadable bodies

        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData((HttpStatusCode)422)]
        public async Task ShouldGiveTypedErrorForBodyWithInvalidCharset(HttpStatusCode status)
        {
            var apiClient = ApiClientFor(HandlerReturning(() =>
            {
                var content = new ByteArrayContent(Encoding.UTF8.GetBytes("{}"));
                content.Headers.TryAddWithoutValidation("Content-Type", "application/json; charset=nope");
                return new HttpResponseMessage(status) { Content = content };
            }));

            await Should.ThrowAsync<CheckoutApiException>(() =>
                apiClient.Get<ContentsResponse>("/test", new SdkAuthorization(PlatformType.Default, ValidDefaultSk)));
        }

        // Log line

        [Theory]
        [InlineData('\u202E')]
        [InlineData('\u200B')]
        [InlineData('\u2029')]
        public void ShouldEscapeUnicodeFormatCharactersInTheLoggedPath(char c)
        {
            ApiClient.LoggablePath("payments/pay_1" + c + "x").ShouldNotContain(c.ToString());
        }

        // Session secret

        [Theory]
        [InlineData("x\r\nInjected: y")]
        [InlineData("sek_ab cd")]
        [InlineData("sek_ab\tcd")]
        [InlineData("sek_ab,cd")]
        [InlineData("sek_é")]
        public void ShouldRejectSessionSecretOutsideTokenCharacters(string secret)
        {
            var ex = Should.Throw<CheckoutArgumentException>(() => new SessionSecretSdkCredentials(secret));
            ex.Message.ShouldNotContain(secret);
        }

        [Fact]
        public void ShouldRejectOverlongSessionSecret()
        {
            Should.Throw<CheckoutArgumentException>(() => new SessionSecretSdkCredentials(new string('a', 4097)));
        }

        [Theory]
        [InlineData("sessionSecret")]
        [InlineData("sek_Abc-123._~+/=")]
        public void ShouldAcceptSessionSecretWithTokenCharacters(string secret)
        {
            new SessionSecretSdkCredentials(secret).Secret.ShouldBe(secret);
        }

        [Newtonsoft.Json.JsonConverter(typeof(ThrowingConverter))]
        public class ThrowingConverterResponse : HttpMetadata
        {
        }

        // Stands in for the SDK's polymorphic converters, which throw non-Json exceptions
        // (bare Exception, InvalidCastException) on unexpected payloads.
        private sealed class ThrowingConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType) => true;

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue,
                Newtonsoft.Json.JsonSerializer serializer)
            {
                throw new InvalidCastException();
            }

            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value,
                Newtonsoft.Json.JsonSerializer serializer)
            {
            }
        }

        public class NoDefaultConstructorResponse : HttpMetadata
        {
            public NoDefaultConstructorResponse(string value)
            {
            }
        }

        private sealed class CapturingLoggerFactory : ILoggerFactory
        {
            public ConcurrentQueue<string> Messages { get; } = new ConcurrentQueue<string>();

            public ILogger CreateLogger(string categoryName)
            {
                return categoryName == typeof(ApiClient).FullName
                    ? new CapturingLogger(Messages)
                    : (ILogger)Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
            }

            public void AddProvider(ILoggerProvider provider)
            {
            }

            public void Dispose()
            {
            }
        }

        private sealed class CapturingLogger : ILogger
        {
            private readonly ConcurrentQueue<string> _messages;

            public CapturingLogger(ConcurrentQueue<string> messages)
            {
                _messages = messages;
            }

            public IDisposable BeginScope<TState>(TState state) => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
                Func<TState, Exception, string> formatter)
            {
                _messages.Enqueue(formatter(state, exception));
            }
        }
    }
}
