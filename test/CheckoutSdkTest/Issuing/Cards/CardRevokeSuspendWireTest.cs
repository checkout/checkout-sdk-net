using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Issuing.Cards.Requests.Revoke;
using Checkout.Issuing.Cards.Requests.Suspend;
using Moq;
using Moq.Protected;
using Shouldly;
using Xunit;

namespace Checkout.Issuing.Cards
{
    /// <summary>
    /// The revoke-card-request and suspend-card-request bodies are optional. Drives the real
    /// <see cref="ApiClient"/> through a stubbed <see cref="HttpMessageHandler"/> to assert what
    /// reaches the wire: no body when the request is null, the JSON body otherwise.
    /// </summary>
    public class CardRevokeSuspendWireTest : IDisposable
    {
        private const string ValidSandboxSk = "sk_sbox_m73dzbpy7cf3gfd46xr4yj5xo4e";

        private readonly HttpClient _httpClient;
        private readonly ApiClient _apiClient;
        private readonly SdkAuthorization _authorization;
        private HttpRequestMessage _capturedRequest;
        private string _capturedBody;

        public CardRevokeSuspendWireTest()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                {
                    _capturedRequest = request;
                    _capturedBody = request.Content?.ReadAsStringAsync().Result;
                })
                .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"_links\":{}}", Encoding.UTF8, "application/json")
                });

            _httpClient = new HttpClient(handler.Object);
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.CreateClient()).Returns(_httpClient);

            _apiClient = new ApiClient(factory.Object, new Uri("https://api.example.com"), false);
            _authorization = new SdkAuthorization(PlatformType.Default, ValidSandboxSk);
        }

        [Theory]
        [InlineData("revoke")]
        [InlineData("suspend")]
        public async Task ShouldSendNoBodyWhenRequestIsNull(string action)
        {
            await _apiClient.Post<Resource>($"issuing/cards/crd_123/{action}", _authorization, null,
                CancellationToken.None, null);

            _capturedRequest.Method.ShouldBe(HttpMethod.Post);
            _capturedRequest.Content.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldSendRevokeReasonWhenProvided()
        {
            await _apiClient.Post<Resource>("issuing/cards/crd_123/revoke", _authorization,
                new RevokeCardRequest { Reason = RevokeReason.ReportedLost }, CancellationToken.None, null);

            _capturedBody.ShouldBe("{\"reason\":\"reported_lost\"}");
        }

        [Fact]
        public async Task ShouldSendSuspendReasonWhenProvided()
        {
            await _apiClient.Post<Resource>("issuing/cards/crd_123/suspend", _authorization,
                new SuspendCardRequest { Reason = SuspendReason.SuspectedStolen }, CancellationToken.None, null);

            _capturedBody.ShouldBe("{\"reason\":\"suspected_stolen\"}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
