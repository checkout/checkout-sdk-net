using Checkout.HandlePaymentsAndPayouts.GooglePay;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Requests;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Responses;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay
{
    public class GooglePayClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public GooglePayClientTest()
        {
            _sdkCredentials.Setup(c => c.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);
            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateEnrollment_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            var request = new GooglePayEnrollmentRequest
            {
                EntityId = "ent_test123",
                EmailAddress = "test@example.com",
                AcceptTermsOfService = true
            };
            var expectedResponse = new GooglePayEnrollmentResponse
            {
                TosAcceptedTime = new System.DateTime(2024, 10, 2, 15, 1, 23, System.DateTimeKind.Utc),
                State = Entities.GooglePayEnrollmentState.Active
            };

            _apiClient.Setup(c => c.Post<GooglePayEnrollmentResponse>(
                    "googlepay/enrollments",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var response = await client.CreateEnrollment(request);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        public async Task CreateEnrollment_WhenRequestIsNull_ShouldThrowCheckoutArgumentException()
        {
            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreateEnrollment(null));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task RegisterDomain_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            const string entityId = "ent_test123";
            var request = new GooglePayRegisterDomainRequest { WebDomain = "example.com" };
            var expectedResponse = new EmptyResponse();

            _apiClient.Setup(c => c.Post<EmptyResponse>(
                    $"googlepay/enrollments/{entityId}/domain",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var response = await client.RegisterDomain(entityId, request);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(expectedResponse);
        }

        [Fact]
        public async Task RegisterDomain_WhenEntityIdIsNull_ShouldThrowCheckoutArgumentException()
        {
            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.RegisterDomain(null, new GooglePayRegisterDomainRequest()));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task RegisterDomain_WhenRequestIsNull_ShouldThrowCheckoutArgumentException()
        {
            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.RegisterDomain("ent_test123", null));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetDomains_WhenEntityIdIsValid_ShouldCallApiClientGet()
        {
            const string entityId = "ent_test123";
            var expectedResponse = new GooglePayDomainListResponse
            {
                Domains = new List<string> { "example.com", "shop.example.com" }
            };

            _apiClient.Setup(c => c.Get<GooglePayDomainListResponse>(
                    $"googlepay/enrollments/{entityId}/domains",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetDomains(entityId);

            response.ShouldNotBeNull();
            response.Domains.ShouldNotBeNull();
            response.Domains.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetDomains_WhenEntityIdIsNull_ShouldThrowCheckoutArgumentException()
        {
            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.GetDomains(null));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task GetEnrollmentState_WhenEntityIdIsValid_ShouldCallApiClientGet()
        {
            const string entityId = "ent_test123";
            var expectedResponse = new GooglePayEnrollmentStateResponse
            {
                State = Entities.GooglePayEnrollmentState.Active
            };

            _apiClient.Setup(c => c.Get<GooglePayEnrollmentStateResponse>(
                    $"googlepay/enrollments/{entityId}/state",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetEnrollmentState(entityId);

            response.ShouldNotBeNull();
            response.State.ShouldBe(Entities.GooglePayEnrollmentState.Active);
        }

        [Fact]
        public async Task GetEnrollmentState_WhenEntityIdIsNull_ShouldThrowCheckoutArgumentException()
        {
            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.GetEnrollmentState(null));
            exception.ShouldBeOfType<CheckoutArgumentException>();
        }

        [Fact]
        public async Task CreateEnrollment_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            var request = new GooglePayEnrollmentRequest
            {
                EntityId = "ent_test123",
                EmailAddress = "test@example.com",
                AcceptTermsOfService = true
            };
            var cancellationToken = new CancellationToken();
            var expectedResponse = new GooglePayEnrollmentResponse();

            _apiClient.Setup(c => c.Post<GooglePayEnrollmentResponse>(
                    "googlepay/enrollments",
                    _authorization,
                    request,
                    cancellationToken,
                    null))
                .ReturnsAsync(expectedResponse);

            IGooglePayClient client = new GooglePayClient(_apiClient.Object, _configuration.Object);
            var response = await client.CreateEnrollment(request, cancellationToken);

            response.ShouldNotBeNull();
        }
    }
}
