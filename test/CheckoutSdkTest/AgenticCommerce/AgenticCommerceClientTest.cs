using Checkout.Accounts;
using Checkout.AgenticCommerce;
using Checkout.AgenticCommerce.Entities;
using Checkout.AgenticCommerce.Requests;
using Checkout.AgenticCommerce.Responses;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.AgenticCommerce
{
    public class AgenticCommerceClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public AgenticCommerceClientTest()
        {
            _sdkCredentials.Setup(c => c.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);
            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateDelegatedPayment_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            var request = CreateValidDelegatedPaymentRequest();
            var headers = CreateValidDelegatedPaymentHeaders();
            var expectedResponse = new DelegatedPaymentResponse
            {
                Id = "vt_abc123def456ghi789",
                Created = new DateTime(2026, 3, 11, 10, 30, 0),
                Metadata = new Dictionary<string, string> { { "psp", "checkout.com" } }
            };

            _apiClient.Setup(c => c.Post<DelegatedPaymentResponse>(
                    "agentic_commerce/delegate_payment",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null,
                    headers))
                .ReturnsAsync(expectedResponse);

            IAgenticCommerceClient client = new AgenticCommerceClient(_apiClient.Object, _configuration.Object);
            var response = await client.CreateDelegatedPayment(request, headers);

            response.ShouldNotBeNull();
            response.Id.ShouldBe("vt_abc123def456ghi789");
            response.Metadata.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateDelegatedPayment_WhenRequestIsNull_ShouldThrowCheckoutArgumentException()
        {
            IAgenticCommerceClient client = new AgenticCommerceClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreateDelegatedPayment(null, new DelegatedPaymentHeaders()));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateDelegatedPayment_WhenHeadersIsNull_ShouldThrowCheckoutArgumentException()
        {
            IAgenticCommerceClient client = new AgenticCommerceClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreateDelegatedPayment(CreateValidDelegatedPaymentRequest(), null));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateDelegatedPayment_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            var request = CreateValidDelegatedPaymentRequest();
            var headers = CreateValidDelegatedPaymentHeaders();
            var cancellationToken = new CancellationToken();
            var expectedResponse = new DelegatedPaymentResponse { Id = "vt_test123" };

            _apiClient.Setup(c => c.Post<DelegatedPaymentResponse>(
                    "agentic_commerce/delegate_payment",
                    _authorization,
                    request,
                    cancellationToken,
                    null,
                    headers))
                .ReturnsAsync(expectedResponse);

            IAgenticCommerceClient client = new AgenticCommerceClient(_apiClient.Object, _configuration.Object);
            var response = await client.CreateDelegatedPayment(request, headers, cancellationToken);

            response.ShouldNotBeNull();
            response.Id.ShouldBe("vt_test123");
        }

        private DelegatedPaymentRequest CreateValidDelegatedPaymentRequest()
        {
            return new DelegatedPaymentRequest
            {
                PaymentMethod = new DelegatedPaymentMethodCard
                {
                    CardNumberType = DelegatedCardNumberType.Fpan,
                    Number = "4242424242424242",
                    ExpMonth = "11",
                    ExpYear = "2026",
                    Metadata = new Dictionary<string, string> { { "issuing_bank", "test" } }
                },
                Allowance = new DelegatedPaymentAllowance
                {
                    Reason = DelegatedPaymentAllowanceReason.OneTime,
                    MaxAmount = 10000,
                    Currency = "USD",
                    MerchantId = "cli_vkuhvk4vjn2edkps7dfsq6emqm",
                    CheckoutSessionId = "1PQrsT",
                    ExpiresAt = DateTime.UtcNow.AddHours(1)
                },
                RiskSignals = new List<DelegatedPaymentRiskSignal>
                {
                    new DelegatedPaymentRiskSignal { Type = "card_testing", Score = 10, Action = "blocked" }
                },
                Metadata = new Dictionary<string, string> { { "campaign", "q4" } }
            };
        }

        private DelegatedPaymentHeaders CreateValidDelegatedPaymentHeaders()
        {
            return new DelegatedPaymentHeaders
            {
                Signature = "eyJtZX...",
                Timestamp = "2026-03-11T10:30:00Z"
            };
        }
    }
}
