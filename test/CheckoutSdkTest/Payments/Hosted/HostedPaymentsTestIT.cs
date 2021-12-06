using Checkout.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentsTestIT : AbstractPaymentsIntegrationTest
    {
        private const string reference = "ORD-123A";

        [Fact]
        private async Task ShouldCreateHostedPayments()
        {
            var request = CreateHostedPaymentRequest(reference);
            var response = await DefaultApi.HostedPaymentsClient().CreateAsync(request);

            response.ShouldNotBeNull();
            response.Reference.ShouldBe(reference);
            response.Links.ShouldNotBeNull();
            response.Links.ContainsKey("redirect").ShouldNotBeNull();
        }


        private static HostedPaymentRequest CreateHostedPaymentRequest(string reference)
        {
            return new HostedPaymentRequest()
            {
                Amount = 1000L,
                Reference = reference,
                Currency = Currency.GBP,
                Description = "Payment for Gold Necklace",
                Customer = new CustomerRequest
                {
                    Name = "Jack Napier",
                    Email = GenerateRandomEmail()
                },
                ShippingDetails = new ShippingDetails
                {
                    Address = GetAddress(),
                    Phone = GetPhone()
                },
                Billing = new BillingInformation()
                {
                    Address = GetAddress(),
                    Phone = GetPhone()
                },
                Recipient = new PaymentRecipient()
                {
                    AccountNumber = "1234567",
                    Country = CountryCode.ES,
                    DateOfBirth = "1985-05-15",
                    FirstName = "IT",
                    LastName = "TESTING",
                    Zip = "12345"
                },
                Processing = new ProcessingSettings() { Aft = true },
                Products = new Product[]
                {
                    new Product()
                    {
                        Name = "Gold Necklace",
                        Quantity = 1L,
                        Price = 200L
                    }
                },
                Risk = new RiskRequest() { Enabled = false },
                SuccessUrl = "https://example.com/payments/success",
                CancelUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/success",
                Locale = "en-GB",
                ThreeDS = new ThreeDsRequest()
                {
                    Enabled = false,
                    AttemptN3D = false
                },
                Capture = true,
                CaptureOn = DateTime.UtcNow
            };

        }

        private static Phone GetPhone()
        {
            return new Phone()
            {
                CountryCode = "1",
                Number = "4155552671"
            };
        }

        private static Address GetAddress()
        {
            return new Address()
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };
        }
    }
}
