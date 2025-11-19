using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Setups;
using Checkout.Payments.Setups.Entities;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    public class PaymentSetupsIntegrationTest : SandboxTestFixture
    {
        public PaymentSetupsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        public async Task CreatePaymentSetup_ShouldReturnValidResponse()
        {
            // Arrange
            var paymentSetupsRequest = CreateValidPaymentSetupsRequest();

            // Act
            var response = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(paymentSetupsRequest);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.ProcessingChannelId.ShouldBe(paymentSetupsRequest.ProcessingChannelId);
            response.Amount.ShouldBe(paymentSetupsRequest.Amount);
            response.Currency.ShouldBe(paymentSetupsRequest.Currency);
            response.PaymentType.ShouldBe(paymentSetupsRequest.PaymentType);
            response.Reference.ShouldBe(paymentSetupsRequest.Reference);
            response.Description.ShouldBe(paymentSetupsRequest.Description);
        }

        [Fact]
        public async Task UpdatePaymentSetup_ShouldReturnValidResponse()
        {
            // Arrange
            var paymentSetupsRequest = CreateValidPaymentSetupsRequest();
            var createResponse = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(paymentSetupsRequest);

            var updateRequest = CreateValidPaymentSetupsRequest();
            updateRequest.Description = "Updated description";

            // Act
            var response = await DefaultApi.PaymentSetupsClient().UpdatePaymentSetup(createResponse.Id, updateRequest);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(createResponse.Id);
            response.Description.ShouldBe("Updated description");
        }

        [Fact]
        public async Task GetPaymentSetup_ShouldReturnValidResponse()
        {
            // Arrange
            var paymentSetupsRequest = CreateValidPaymentSetupsRequest();
            var createResponse = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(paymentSetupsRequest);

            // Act
            var response = await DefaultApi.PaymentSetupsClient().GetPaymentSetup(createResponse.Id);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(createResponse.Id);
            response.ProcessingChannelId.ShouldBe(paymentSetupsRequest.ProcessingChannelId);
            response.Amount.ShouldBe(paymentSetupsRequest.Amount);
            response.Currency.ShouldBe(paymentSetupsRequest.Currency);
            response.PaymentType.ShouldBe(paymentSetupsRequest.PaymentType);
            response.Reference.ShouldBe(paymentSetupsRequest.Reference);
            response.Description.ShouldBe(paymentSetupsRequest.Description);
        }

        [Fact(Skip = "Integration test - requires valid payment method option")]
        public async Task ConfirmPaymentSetup_ShouldReturnValidResponse()
        {
            // Arrange
            var paymentSetupsRequest = CreateValidPaymentSetupsRequest();
            var createResponse = await DefaultApi.PaymentSetupsClient().CreatePaymentSetup(paymentSetupsRequest);

            // This would require extracting a payment method option ID from the create response
            var paymentMethodOptionId = "opt_test_12345"; // This should come from the payment setup response

            // Act
            var response = await DefaultApi.PaymentSetupsClient().ConfirmPaymentSetup(createResponse.Id, paymentMethodOptionId);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.ActionId.ShouldNotBeNull();
            response.Amount.ShouldBe(paymentSetupsRequest.Amount);
            response.Currency.ShouldBe(paymentSetupsRequest.Currency);
            response.ProcessedOn.ShouldNotBeNull();
        }

        private PaymentSetupsRequest CreateValidPaymentSetupsRequest()
        {
            return new PaymentSetupsRequest
            {
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                Amount = 1000,
                Currency = Currency.GBP,
                PaymentType = PaymentType.Regular,
                Reference = $"TEST-REF-{RandomString(6)}",
                Description = "Integration test payment setup",
                Settings = new Settings
                {
                    SuccessUrl = "https://example.com/success",
                    FailureUrl = "https://example.com/failure"
                },
                Customer = new Customer
                {
                    Name = "John Smith",
                    Email = new CustomerEmail
                    {
                        Address = $"john.smith+{RandomString(6)}@example.com",
                        Verified = true
                    },
                    Phone = new Phone
                    {
                        CountryCode = "+44",
                        Number = "207 946 0000"
                    },
                    Device = new CustomerDevice
                    {
                        Locale = "en_GB"
                    }
                },
                PaymentMethods = new PaymentMethods
                {
                    // Configure basic payment methods for testing
                    Klarna = new Klarna
                    {
                        Initialization = PaymentMethodInitialization.Disabled,
                        AccountHolder = new KlarnaAccountHolder
                        {
                            BillingAddress = new Address
                            {
                                AddressLine1 = "123 High Street",
                                City = "London",
                                Zip = "SW1A 1AA",
                                Country = CountryCode.GB
                            }
                        }
                    }
                }
            };
        }
    }
}