using Checkout.HandlePaymentsAndPayouts.ApplePay.Entities;
using Checkout.HandlePaymentsAndPayouts.ApplePay.Requests;
using Checkout.HandlePaymentsAndPayouts.ApplePay.Responses;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.ApplePay
{
    public class ApplePayEnrollIntegrationTest : SandboxTestFixture
    {
        public ApplePayEnrollIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        public async Task EnrollDomain_ShouldSucceed()
        {
            // Arrange
            var request = CreateValidEnrollDomainRequest();

            // Act
            var response = await DefaultApi.ApplePayClient().EnrollDomain(request);

            // Assert
            response.ShouldNotBeNull();
            // EmptyResponse doesn't have properties to validate, but should not throw
        }

        // Common methods
        private EnrollDomainRequest CreateValidEnrollDomainRequest()
        {
            return new EnrollDomainRequest
            {
                Domain = "checkout-test-domain.com"
            };
        }
    }
}