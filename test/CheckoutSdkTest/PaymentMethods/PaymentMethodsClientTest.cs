using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;

using Checkout.PaymentMethods.Entities;
using Checkout.PaymentMethods.Requests;
using Checkout.PaymentMethods.Responses;

namespace Checkout.PaymentMethods
{
    public class PaymentMethodsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        private const string _processingChannelId = "pc_test_123456";

        public PaymentMethodsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task GetAvailablePaymentMethods_WhenProcessingChannelIdIsValid_ShouldCallApiClientQuery()
        {
            // Arrange
            var expectedResponse = CreateGetAvailablePaymentMethodsResponse();

            _apiClient.Setup(apiClient => apiClient.Query<GetAvailablePaymentMethodsResponse>(
                    "payment-methods",
                    _authorization,
                    It.Is<PaymentMethodsQueryFilter>(f => f.ProcessingChannelId == _processingChannelId),
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IPaymentMethodsClient paymentMethodsClient = new PaymentMethodsClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await paymentMethodsClient.GetAvailablePaymentMethods(_processingChannelId);

            // Assert
            ValidateGetAvailablePaymentMethodsResponse(response, expectedResponse);
        }

        [Fact]
        public async Task GetAvailablePaymentMethods_WhenProcessingChannelIdIsNull_ShouldThrowCheckoutArgumentException()
        {
            // Arrange
            IPaymentMethodsClient paymentMethodsClient = new PaymentMethodsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await paymentMethodsClient.GetAvailablePaymentMethods(null));
        }

        [Fact]
        public async Task GetAvailablePaymentMethods_WhenProcessingChannelIdIsEmpty_ShouldThrowCheckoutArgumentException()
        {
            // Arrange
            IPaymentMethodsClient paymentMethodsClient = new PaymentMethodsClient(_apiClient.Object, _configuration.Object);

            // Act & Assert
            await Should.ThrowAsync<CheckoutArgumentException>(async () => await paymentMethodsClient.GetAvailablePaymentMethods(string.Empty));
        }

        [Fact]
        public async Task GetAvailablePaymentMethods_WithCancellationToken_ShouldPassTokenToApiClient()
        {
            // Arrange
            var expectedResponse = CreateGetAvailablePaymentMethodsResponse();
            var cancellationToken = new CancellationToken();

            _apiClient.Setup(apiClient => apiClient.Query<GetAvailablePaymentMethodsResponse>(
                    "payment-methods",
                    _authorization,
                    It.Is<PaymentMethodsQueryFilter>(f => f.ProcessingChannelId == _processingChannelId),
                    cancellationToken))
                .ReturnsAsync(expectedResponse);

            IPaymentMethodsClient paymentMethodsClient = new PaymentMethodsClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await paymentMethodsClient.GetAvailablePaymentMethods(_processingChannelId, cancellationToken);

            // Assert
            ValidateGetAvailablePaymentMethodsResponse(response, expectedResponse);
        }

        // Common methods
        private GetAvailablePaymentMethodsResponse CreateGetAvailablePaymentMethodsResponse()
        {
            return new GetAvailablePaymentMethodsResponse
            {
                Methods = new List<PaymentMethod>
                {
                    new PaymentMethod
                    {
                        Type = PaymentMethodType.Visa,
                        PartnerMerchantId = "merchant_123"
                    },
                    new PaymentMethod
                    {
                        Type = PaymentMethodType.Klarna,
                        PartnerMerchantId = "merchant_456"
                    }
                }
            };
        }

        private void ValidateGetAvailablePaymentMethodsResponse(GetAvailablePaymentMethodsResponse actual, GetAvailablePaymentMethodsResponse expected)
        {
            actual.ShouldNotBeNull();
            actual.ShouldBeSameAs(expected);
            actual.Methods.ShouldNotBeNull();
            actual.Methods.Count.ShouldBe(expected.Methods.Count);
        }
    }
}