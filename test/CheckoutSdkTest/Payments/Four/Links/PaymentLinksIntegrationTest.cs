using Checkout.Common;
using Checkout.Payments.Links;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Four.Links
{
    public class PaymentLinksIntegrationTest : SandboxTestFixture
    {
        public PaymentLinksIntegrationTest() : base(PlatformType.Four)
        {
        }

        [Fact]
        private async Task ShouldCreateAndGetPaymentLink()
        {
            var paymentLinkRequest = new PaymentLinkRequest
            {
                Amount = 10,
                Billing = new BillingInformation {Address = GetAddress()},
                Capture = true,
                CaptureOn = DateTime.Now,
                Currency = Currency.GBP,
                Customer = new CustomerRequest {Id = "Id", Email = GenerateRandomEmail(), Name = "name"},
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
                AllowPaymentMethods = new List<PaymentSourceType> {PaymentSourceType.Card, PaymentSourceType.Ideal}
            };

            var response = await FourApi.PaymentLinksClient().Create(paymentLinkRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Links.Count.ShouldBe(2);
            response.Reference.ShouldNotBeNull();
            response.Warnings.Count.ShouldBe(1);

            var responseGet = await FourApi.PaymentLinksClient().Get(response.Id);

            responseGet.ShouldNotBeNull();
            responseGet.Id.ShouldNotBeNull();
            responseGet.Amount.ShouldBe(10);
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