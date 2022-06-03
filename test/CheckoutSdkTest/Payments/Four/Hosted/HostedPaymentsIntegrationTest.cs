using Checkout.Common;
using Checkout.Payments.Hosted;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Four.Hosted
{
    public class HostedPaymentsIntegrationTest : SandboxTestFixture
    {
        public HostedPaymentsIntegrationTest() : base(PlatformType.Four)
        {
        }

        [Fact]
        private async Task ShouldCreateAndGetHostedPayment()
        {
            var hostedPaymentRequest = CreateHostedPaymentRequest();

            var createResponse = await FourApi.HostedPaymentsClient().Create(hostedPaymentRequest);

            createResponse.ShouldNotBeNull();
            createResponse.Id.ShouldNotBeNullOrEmpty();
            createResponse.Reference.ShouldNotBeNullOrEmpty();
            createResponse.Links.ShouldNotBeNull();
            createResponse.Links.ContainsKey("redirect").ShouldBeTrue();
            createResponse.Warnings.Count.ShouldBe(1);

            var getResponse = await FourApi.HostedPaymentsClient().Get(createResponse.Id);

            getResponse.ShouldNotBeNull();
            getResponse.Id.ShouldNotBeNullOrEmpty();
            getResponse.Reference.ShouldNotBeNullOrEmpty();
            getResponse.Status.ShouldBe(HostedPaymentStatus.PaymentPending);
            getResponse.Amount.ShouldNotBeNull();
            getResponse.Billing.ShouldNotBeNull();
            getResponse.Currency.ShouldBe(Currency.GBP);
            getResponse.Customer.ShouldNotBeNull();
            getResponse.Description.ShouldNotBeNullOrEmpty();
            getResponse.FailureUrl.ShouldNotBeNull();
            getResponse.SuccessUrl.ShouldNotBeNull();
            getResponse.CancelUrl.ShouldNotBeNullOrEmpty();
            getResponse.Links.Count.ShouldBe(2);
            getResponse.Products.Count.ShouldBe(1);
        }

        protected static HostedPaymentRequest CreateHostedPaymentRequest()
        {
            var customer = new CustomerRequest {Name = "Jack Napier", Email = GenerateRandomEmail()};

            var shippingDetails = new ShippingDetails {Address = GetAddress(), Phone = GetPhone()};

            var billing = new BillingInformation {Address = GetAddress(), Phone = GetPhone()};

            var recipient = new PaymentRecipient
            {
                AccountNumber = "1234567",
                DateOfBirth = "1985-05-15",
                LastName = "TESTING",
                Zip = "12345"
            };

            var products = new[] {new Product {Name = "Gold Necklace", Quantity = 1L, Price = 200L}};

            return new HostedPaymentRequest
            {
                Amount = 1000L,
                Reference = "reference",
                Currency = Currency.GBP,
                Description = "Payment for Gold Necklace",
                Customer = customer,
                Shipping = shippingDetails,
                Billing = billing,
                Recipient = recipient,
                Processing = new ProcessingSettings {Aft = true},
                Products = products,
                Risk = new RiskRequest {Enabled = false},
                SuccessUrl = "https://example.com/payments/success",
                CancelUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/success",
                Locale = "en-GB",
                ThreeDs = new ThreeDsRequest {Enabled = false, AttemptN3D = false},
                Capture = true,
                CaptureOn = DateTime.UtcNow,
                AllowPaymentMethods =
                    new List<PaymentSourceType> {PaymentSourceType.Card, PaymentSourceType.Ideal}
            };
        }
    }
}