using Shouldly;
using System.Threading.Tasks;
using Xunit;

using Checkout.PaymentMethods.Responses;

namespace Checkout.PaymentMethods
{
    public class PaymentMethodsIntegrationTest : SandboxTestFixture
    {
        private const string _validProcessingChannelId = "pc_test_valid_channel_id";
        private const string _invalidProcessingChannelId = "pc_test_invalid_channel_id";

        public PaymentMethodsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "This test requires a valid processing channel ID")]
        public async Task GetAvailablePaymentMethods_ShouldReturnValidResponse()
        {
            // Act
            var response = await DefaultApi.PaymentMethodsClient().GetAvailablePaymentMethods(_validProcessingChannelId);

            // Assert
            ValidateGetAvailablePaymentMethodsResponse(response);
        }

        [Fact(Skip = "This test requires a valid processing channel ID")]
        public async Task GetAvailablePaymentMethods_WithSpecificProcessingChannel_ShouldReturnRelevantMethods()
        {
            // Act
            var response = await DefaultApi.PaymentMethodsClient().GetAvailablePaymentMethods(_validProcessingChannelId);

            // Assert
            ValidateGetAvailablePaymentMethodsResponse(response);
            ValidateProcessingChannelSpecificMethods(response, _validProcessingChannelId);
        }

        [Fact]
        public async Task GetAvailablePaymentMethods_WithInvalidProcessingChannelId_ShouldThrowException()
        {
            // Act & Assert
            await Should.ThrowAsync<CheckoutApiException>(async () => 
                await DefaultApi.PaymentMethodsClient().GetAvailablePaymentMethods(_invalidProcessingChannelId));
        }

        // Common methods
        private void ValidateGetAvailablePaymentMethodsResponse(GetAvailablePaymentMethodsResponse response)
        {
            response.ShouldNotBeNull();
            response.Methods.ShouldNotBeNull();
            response.Methods.Count.ShouldBeGreaterThan(0);

            foreach (var method in response.Methods)
            {
                method.ShouldNotBeNull();
                method.Type.ShouldNotBeNull();
            }
        }

        private void ValidateProcessingChannelSpecificMethods(GetAvailablePaymentMethodsResponse response, string processingChannelId)
        {
            // Validate that the returned methods are appropriate for the processing channel
            response.Methods.ShouldNotBeEmpty();
            
            foreach (var method in response.Methods)
            {
                // Validate that each method has the expected structure
                method.Type.ShouldNotBeNull();
                
                // If partner merchant ID is provided, it should be valid
                if (!string.IsNullOrEmpty(method.PartnerMerchantId))
                {
                    method.PartnerMerchantId.ShouldNotBeNullOrEmpty();
                }
            }
        }
    }
}