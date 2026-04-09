using Checkout.ComplianceRequests;
using Checkout.ComplianceRequests.Requests;
using Checkout.ComplianceRequests.Responses;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.ComplianceRequests
{
    public class ComplianceRequestsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public ComplianceRequestsClientTest()
        {
            _sdkCredentials.Setup(c => c.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);
            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task GetComplianceRequest_WhenPaymentIdIsValid_ShouldCallApiClientGet()
        {
            const string paymentId = "pay_fun26akvvjjerahhctaq2uzhu4";
            var expectedResponse = new ComplianceRequestDetailsResponse
            {
                PaymentId = paymentId,
                Status = "pending",
                Amount = "38.23",
                Currency = "HKD"
            };

            _apiClient.Setup(c => c.Get<ComplianceRequestDetailsResponse>(
                    $"compliance-requests/{paymentId}",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IComplianceRequestsClient client = new ComplianceRequestsClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetComplianceRequest(paymentId);

            response.ShouldNotBeNull();
            response.PaymentId.ShouldBe(paymentId);
            response.Status.ShouldBe("pending");
            response.Amount.ShouldBe("38.23");
        }

        [Fact]
        public async Task GetComplianceRequest_WhenPaymentIdIsNull_ShouldThrowCheckoutArgumentException()
        {
            IComplianceRequestsClient client = new ComplianceRequestsClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.GetComplianceRequest(null));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task RespondToComplianceRequest_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            const string paymentId = "pay_fun26akvvjjerahhctaq2uzhu4";
            var request = new ComplianceRequestRespondRequest
            {
                Fields = new ComplianceRespondedFields
                {
                    Sender = new List<ComplianceRespondedField>
                    {
                        new ComplianceRespondedField { Name = "date_of_birth", Value = "2000-01-01", NotAvailable = false }
                    }
                },
                Comments = "Responding to compliance request"
            };
            var expectedResponse = new EmptyResponse();

            _apiClient.Setup(c => c.Post<EmptyResponse>(
                    $"compliance-requests/{paymentId}",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IComplianceRequestsClient client = new ComplianceRequestsClient(_apiClient.Object, _configuration.Object);
            var response = await client.RespondToComplianceRequest(paymentId, request);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        public async Task RespondToComplianceRequest_WhenPaymentIdIsNull_ShouldThrowCheckoutArgumentException()
        {
            IComplianceRequestsClient client = new ComplianceRequestsClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.RespondToComplianceRequest(null, new ComplianceRequestRespondRequest()));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task RespondToComplianceRequest_WhenRequestIsNull_ShouldThrowCheckoutArgumentException()
        {
            IComplianceRequestsClient client = new ComplianceRequestsClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.RespondToComplianceRequest("pay_test123", null));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetComplianceRequest_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            const string paymentId = "pay_fun26akvvjjerahhctaq2uzhu4";
            var cancellationToken = new CancellationToken();
            var expectedResponse = new ComplianceRequestDetailsResponse { PaymentId = paymentId };

            _apiClient.Setup(c => c.Get<ComplianceRequestDetailsResponse>(
                    $"compliance-requests/{paymentId}",
                    _authorization,
                    cancellationToken))
                .ReturnsAsync(expectedResponse);

            IComplianceRequestsClient client = new ComplianceRequestsClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetComplianceRequest(paymentId, cancellationToken);

            response.ShouldNotBeNull();
            response.PaymentId.ShouldBe(paymentId);
        }
    }
}
