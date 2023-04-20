using Checkout.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Links
{
    public class PaymentLinksIntegrationTest : SandboxTestFixture
    {
        public PaymentLinksIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldCreateAndGetPaymentLink()
        {
            var paymentLinkRequest = new PaymentLinkRequest
            {
                Amount = 100,
                Billing = new BillingInformation { Address = GetAddress() },
                Capture = true,
                CaptureOn = DateTime.Now,
                Currency = Currency.GBP,
                Customer = new CustomerRequest { Id = "Id", Email = GenerateRandomEmail(), Name = "name" },
                Description = "description",
                ExpiresIn = 1,
                Locale = "locale",
                Reference = "referene",
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
                PaymentType = PaymentType.Regular,
                AllowPaymentMethods =
                    new List<PaymentSourceType> { PaymentSourceType.Card, PaymentSourceType.Ideal },
                AmountAllocations = new List<AmountAllocations>
                {
                    new AmountAllocations
                    {
                        Id = "ent_sdioy6bajpzxyl3utftdp7legq",
                        Amount = 100,
                        Reference = Guid.NewGuid().ToString(),
                        Commission = new Commission { Amount = 1, Percentage = 0.1 }
                    }
                }
            };

            var response = await DefaultApi.PaymentLinksClient().Create(paymentLinkRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Links.Count.ShouldBe(2);
            response.Reference.ShouldNotBeNull();
            response.Warnings.Count.ShouldBe(1);

            var responseGet = await DefaultApi.PaymentLinksClient().Get(response.Id);

            responseGet.ShouldNotBeNull();
            responseGet.Id.ShouldNotBeNull();
            responseGet.Amount.ShouldBe(100);
            responseGet.Billing.ShouldNotBeNull();
            responseGet.CreatedOn.ShouldNotBeNull();
            responseGet.Currency.ShouldNotBeNull();
            responseGet.Customer.ShouldNotBeNull();
            responseGet.Status.ShouldNotBeNull();
            responseGet.Reference.ShouldNotBeNullOrEmpty();
            responseGet.Description.ShouldNotBeNullOrEmpty();
            responseGet.Links.ShouldNotBeNull();
            responseGet.Links.Count.ShouldBe(2);
            responseGet.Reference.ShouldNotBeNull();
            responseGet.Metadata.ShouldNotBeNull();
            responseGet.Metadata.Count.ShouldBe(0);
        }
    }
}