using Checkout.Payments.Links;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.SDK.UnitTest.Payments.Links
{

    public class PaymentLinksClientTest : UnitTestFixture
    {
        private const string PaymentLinksPath = "payment-links";

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public PaymentLinksClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Production, _httpClientFactory.Object);
        }

        [Fact]
        public async Task ShouldCreatePaymentLinks()
        {
            PaymentLinkRequest paymentLinkRequest = new PaymentLinkRequest();
            PaymentLinkResponse paymentLinkResponse = new PaymentLinkResponse
            {
                Id = "1",
                ExpiresOn="2021",
                Reference="ref1234"
            };


            _apiClient.Setup(apiClient =>
                   apiClient.Post<PaymentLinkResponse>(PaymentLinksPath, _authorization, paymentLinkRequest, CancellationToken.None,null))
                       .ReturnsAsync(() => paymentLinkResponse);
            
            IPaymentLinksClient paymentLinksClient = new PaymentLinksClient(_apiClient.Object, _configuration.Object);

            var response = await paymentLinksClient.CreateAsync(paymentLinkRequest,CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentLinkResponse);

        }


        [Fact]
        public async Task ShouldRetrievePaymentLinks()
        {
            var reference = "ref12345";

            var paymentLinkDetailsResponse = new PaymentLinkDetailsResponse()
            {
                Id = "1",
                ExpiresOn = "2021",
                Description = "Test",
                ReturnUrl = "test.com",
                Reference = reference
            };
            
            _apiClient.Setup(apiClient =>
                    apiClient.Get<PaymentLinkDetailsResponse>(PaymentLinksPath + "/" + reference, _authorization, CancellationToken.None))
                        .ReturnsAsync(() => paymentLinkDetailsResponse);

            IPaymentLinksClient paymentLinksClient = new PaymentLinksClient(_apiClient.Object, _configuration.Object);

            var response = await paymentLinksClient.GetAsync(reference, CancellationToken.None);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(paymentLinkDetailsResponse);
        }


    }
}
