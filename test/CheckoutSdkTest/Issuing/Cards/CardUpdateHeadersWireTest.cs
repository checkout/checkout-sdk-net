using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Issuing.Cards.Requests.Update;
using Checkout.Issuing.Cards.Responses.Update;
using Moq;
using Moq.Protected;
using Shouldly;
using Xunit;

namespace Checkout.Issuing.Cards
{
    /// <summary>
    /// Verifies the <c>return-encrypted-cvv</c> and <c>Encryption-Key</c> headers on the actual
    /// outgoing HTTP request.
    ///
    /// The two pre-existing tests for this feature both bypass the code that emits headers:
    /// <c>CardUpdateResponseSerializationTest</c> asserts the JSON serialization of
    /// <see cref="CardUpdateHeaders"/>, which headers never pass through, and
    /// <c>CardClientTest</c> mocks <see cref="IApiClient"/> and so only proves the object was
    /// handed over. Headers are actually applied by reflection in <c>ApiClient</c>, using
    /// <c>ResolveHeaderValue</c> and <c>GetJsonPropertyName</c>. Nothing covered that path, which
    /// is why a boolean header reaching the wire as "True" went unnoticed.
    ///
    /// This drives the real <see cref="ApiClient"/> through a stubbed
    /// <see cref="HttpMessageHandler"/>, following <c>AccountsSchemaVersionHeaderTest</c>.
    /// </summary>
    public class CardUpdateHeadersWireTest : IDisposable
    {
        // Well-formed sandbox key (matches the credentials regex); not a real credential.
        private const string ValidSandboxSk = "sk_sbox_m73dzbpy7cf3gfd46xr4yj5xo4e";
        private const string CardId = "crd_fa6psq42dc0uxl3ct3jryhdo2m";
        private const string PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A";

        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly ApiClient _apiClient;
        private readonly SdkAuthorization _authorization;
        private HttpRequestMessage _capturedRequest;

        public CardUpdateHeadersWireTest()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) => _capturedRequest = request)
                .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"last_modified_date\":\"2026-06-01T10:00:00Z\",\"encrypted_cvv\":\"oJMoNMEEUiQKYOsQ4Zd\"}",
                        Encoding.UTF8,
                        "application/json")
                });

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(factory => factory.CreateClient()).Returns(_httpClient);

            _apiClient = new ApiClient(httpClientFactoryMock.Object, new Uri("https://api.example.com"), false);
            _authorization = new SdkAuthorization(PlatformType.Default, ValidSandboxSk);
        }

        private Task<CardUpdateResponse> Patch(CardUpdateHeaders headers) =>
            _apiClient.Patch<CardUpdateResponse>(
                $"issuing/cards/{CardId}",
                _authorization,
                new CardsUpdateRequest { Reference = "X-123456-N11" },
                CancellationToken.None,
                null,
                headers);

        private string CapturedHeader(string name) =>
            _capturedRequest != null && _capturedRequest.Headers.TryGetValues(name, out var values)
                ? string.Join(",", values)
                : null;

        /// <summary>
        /// The spec types return-encrypted-cvv as a boolean and spells its values "true" and
        /// "false". bool.ToString() in .NET returns "True", and header values are not JSON, so
        /// nothing downstream lower-cases them.
        /// </summary>
        [Fact]
        public async Task ShouldSendReturnEncryptedCvvLowerCase()
        {
            await Patch(new CardUpdateHeaders { ReturnEncryptedCvv = true, EncryptionKey = PublicKey });

            CapturedHeader("return-encrypted-cvv").ShouldBe("true");
            CapturedHeader("return-encrypted-cvv").ShouldNotBe("True");
        }

        [Fact]
        public async Task ShouldSendFalseLowerCase()
        {
            await Patch(new CardUpdateHeaders { ReturnEncryptedCvv = false });

            CapturedHeader("return-encrypted-cvv").ShouldBe("false");
        }

        /// <summary>
        /// Both names are case sensitive in the spec: return-encrypted-cvv is lower case and
        /// Encryption-Key is title case. They come from the Newtonsoft JsonProperty attributes on
        /// the container, resolved by GetJsonPropertyName.
        /// </summary>
        [Fact]
        public async Task ShouldSendBothHeadersWithTheirSpecSpelling()
        {
            await Patch(new CardUpdateHeaders { ReturnEncryptedCvv = true, EncryptionKey = PublicKey });

            var names = _capturedRequest.Headers.Select(header => header.Key).ToList();
            names.ShouldContain("return-encrypted-cvv");
            names.ShouldContain("Encryption-Key");
            names.ShouldNotContain("ReturnEncryptedCvv");
            names.ShouldNotContain("EncryptionKey");

            CapturedHeader("Encryption-Key").ShouldBe(PublicKey);
        }

        [Fact]
        public async Task ShouldSendTheEncryptionKeyOnItsOwn()
        {
            await Patch(new CardUpdateHeaders { EncryptionKey = PublicKey });

            CapturedHeader("Encryption-Key").ShouldBe(PublicKey);
            CapturedHeader("return-encrypted-cvv").ShouldBeNull();
        }

        [Fact]
        public async Task ShouldSendNoCardHeadersWhenNoneAreSupplied()
        {
            await Patch(null);

            CapturedHeader("return-encrypted-cvv").ShouldBeNull();
            CapturedHeader("Encryption-Key").ShouldBeNull();
        }

        /// <summary>
        /// An unset nullable header is skipped rather than emitted empty.
        /// </summary>
        [Fact]
        public async Task ShouldSkipUnsetHeaders()
        {
            await Patch(new CardUpdateHeaders());

            CapturedHeader("return-encrypted-cvv").ShouldBeNull();
            CapturedHeader("Encryption-Key").ShouldBeNull();
        }

        [Fact]
        public async Task ShouldReadEncryptedCvvFromTheResponse()
        {
            var response = await Patch(new CardUpdateHeaders { ReturnEncryptedCvv = true, EncryptionKey = PublicKey });

            response.EncryptedCvv.ShouldBe("oJMoNMEEUiQKYOsQ4Zd");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
