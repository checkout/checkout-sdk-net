using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Accounts.Entities.Request;
using Moq;
using Moq.Protected;
using Shouldly;
using Xunit;

namespace Checkout.Accounts
{
    /// <summary>
    /// Verifies that Accounts onboarding operations negotiate the schema version end-to-end by
    /// emitting the <c>Accept</c> header on the actual outgoing HTTP request. Unlike
    /// <c>AccountsClientTest</c> (which mocks <see cref="IApiClient"/>), this exercises the real
    /// <see cref="ApiClient"/> header-application path via a stubbed <see cref="HttpMessageHandler"/>.
    /// </summary>
    public class AccountsSchemaVersionHeaderTest : IDisposable
    {
        // Well-formed sandbox key (matches the credentials regex); not a real credential.
        private const string ValidSandboxSk = "sk_sbox_m73dzbpy7cf3gfd46xr4yj5xo4e";
        // The value the SDK builds. .NET normalizes the header on the wire (adds a space after the
        // semicolon), so assertions compare whitespace-insensitively to stay framework-agnostic.
        private const string DefaultSchemaVersionAccept = "application/json;schema_version=3.0";

        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly AccountsClient _accountsClient;
        private HttpRequestMessage _capturedRequest;

        public AccountsSchemaVersionHeaderTest()
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
                    Content = new StringContent("{}", Encoding.UTF8, "application/json")
                });

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(factory => factory.CreateClient()).Returns(_httpClient);

            var apiClient = new ApiClient(httpClientFactoryMock.Object, new Uri("https://api.example.com"), false);
            var credentials = new StaticKeysSdkCredentials(ValidSandboxSk, null);
            var configuration = new CheckoutConfiguration(credentials, Environment.Sandbox, httpClientFactoryMock.Object);

            _accountsClient = new AccountsClient(apiClient, apiClient, configuration);
        }

        private string CapturedAccept() =>
            _capturedRequest?.Headers.Accept.ToString().Replace(" ", string.Empty);

        [Fact]
        public async Task ShouldSendDefaultSchemaVersionOnCreateEntity()
        {
            await _accountsClient.CreateEntity(new OnboardEntityRequest());
            CapturedAccept().ShouldBe(DefaultSchemaVersionAccept);
        }

        [Fact]
        public async Task ShouldSendDefaultSchemaVersionOnGetEntity()
        {
            await _accountsClient.GetEntity("ent_123");
            CapturedAccept().ShouldBe(DefaultSchemaVersionAccept);
        }

        [Fact]
        public async Task ShouldSendDefaultSchemaVersionOnUpdateEntity()
        {
            await _accountsClient.UpdateEntity("ent_123", new OnboardEntityRequest());
            CapturedAccept().ShouldBe(DefaultSchemaVersionAccept);
        }

        [Fact]
        public async Task ShouldSendDefaultSchemaVersionOnGetEntityRequirements()
        {
            await _accountsClient.GetEntityRequirements("ent_123");
            CapturedAccept().ShouldBe(DefaultSchemaVersionAccept);
        }

        [Fact]
        public async Task ShouldOverrideSchemaVersionWhenSpecified()
        {
            await _accountsClient.GetEntity("ent_123", schemaVersion: "2.0");
            CapturedAccept().ShouldBe("application/json;schema_version=2.0");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
