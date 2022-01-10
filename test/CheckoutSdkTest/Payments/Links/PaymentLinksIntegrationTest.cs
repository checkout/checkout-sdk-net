using System;
using System.Threading.Tasks;
using Checkout.Common;
using Shouldly;
using Xunit;

namespace Checkout.Payments.Links
{
    public class PaymentLinksIntegrationTest : SandboxTestFixture
    {
        public PaymentLinksIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldCreatePaymentLink()
        {
            var address = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var paymentLinkRequest = new PaymentLinkRequest
            {
                Amount = 10,
                Billing = new BillingInformation
                {
                    Address = address
                },
                Capture = true,
                CaptureOn = DateTime.Now,
                Currency = Currency.GBP,
                Customer = new CustomerRequest
                {
                    Id = "Id",
                    Email = GenerateRandomEmail(),
                    Name = "name"
                },
                Description = "description",
                ExpiresIn = 1,
                Locale = "loc",
                Reference = "ORD-123A",
                ThreeDs = new ThreeDsRequest
                {
                    Enabled = true,
                    AttemptN3D = false,
                    Eci = "05",
                    Cryptogram = "AgAAAAAAAIR8CQrXcIhbQAAAAAA",
                    Xid = "MDAwMDAwMDAwMDAwMDAwMzIyNzY=",
                    Version = "2.0.1",
                    ChallengeIndicator = ChallengeIndicatorType.NoPreference
                },
                PaymentType = PaymentType.Regular                
            };

            var response = await DefaultApi.PaymentLinksClient().Create(paymentLinkRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Links.Count.ShouldBe(2);
            response.Reference.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRetrievePaymentLinks()
        {
            var address = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var paymentLinkRequest = new PaymentLinkRequest
            {
                Amount = 10,
                Billing = new BillingInformation()
                {
                    Address = address
                },
                Capture = true,
                CaptureOn = DateTime.Now,
                Currency = Currency.GBP,
                Customer = new CustomerRequest
                {
                    Id = "Id",
                    Email = GenerateRandomEmail(),
                    Name = "name"
                },
                Description = "description",
                ExpiresIn = 1,
                Locale = "loc",
                Reference = "ORD-123A",
                ThreeDs = new ThreeDsRequest
                {
                    Enabled = true,
                    AttemptN3D = false,
                    Eci = "05",
                    Cryptogram = "AgAAAAAAAIR8CQrXcIhbQAAAAAA",
                    Xid = "MDAwMDAwMDAwMDAwMDAwMzIyNzY=",
                    Version = "2.0.1",
                    ChallengeIndicator = ChallengeIndicatorType.NoPreference
                }
            };

            var responseToPaymentLinksClientToCreate = await DefaultApi.PaymentLinksClient().Create(paymentLinkRequest);
            responseToPaymentLinksClientToCreate.ShouldNotBeNull();
            responseToPaymentLinksClientToCreate.Id.ShouldNotBeNull();
            responseToPaymentLinksClientToCreate.Links.ShouldNotBeNull();
            responseToPaymentLinksClientToCreate.Links.Count.ShouldBe(2);
            responseToPaymentLinksClientToCreate.Reference.ShouldNotBeNull();

            var responseGet = await DefaultApi.PaymentLinksClient().Get(responseToPaymentLinksClientToCreate.Id);

            responseGet.ShouldNotBeNull();
            responseGet.Id.ShouldNotBeNull();
            responseGet.Links.ShouldNotBeNull();
            responseGet.Links.Count.ShouldBe(2);
            responseGet.Reference.ShouldNotBeNull();
            responseGet.Metadata.ShouldNotBeNull();
            responseGet.Metadata.Count.ShouldBe(0);
        }
    }
}