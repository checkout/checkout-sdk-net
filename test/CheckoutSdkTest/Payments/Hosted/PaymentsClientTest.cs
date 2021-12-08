using Checkout.Common;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Hosted
{
    public class PaymentsClientTest : AbstractPaymentsIntegrationTest
    {
        private const string HostedPayments = "/hosted-payments";
        private const string Reference = "ORD-1234";
        private const string ValidDefaultSk = "sk_test_fde517a8-3f01-41ef-b4bd-4282384b0a64";

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<CheckoutConfiguration> _configuration;
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private HostedPaymentResponse _hostedPaymentResponse = new HostedPaymentResponse();


        public PaymentsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
            .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);

            _hostedPaymentResponse.Reference = Reference;
            _hostedPaymentResponse.Links = new Dictionary<string, Link>();
        }        

        [Fact]
        private async Task ShouldCreateHostedPayments()
        {
            var hostedPaymentRequest = CreateHostedPaymentRequest(Reference);            

            _apiClient.Setup(apiClient => 
            apiClient.Post<HostedPaymentResponse>(HostedPayments, _authorization, hostedPaymentRequest, CancellationToken.None, null))
                .ReturnsAsync(() => _hostedPaymentResponse);

            var client = new HostedPaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await client.CreateAsync(hostedPaymentRequest, CancellationToken.None);
            
            _hostedPaymentResponse.ShouldNotBeNull();
            response.Reference.ShouldBe(Reference);
            response.Links.ShouldNotBeNull();
        }        
    }
}
