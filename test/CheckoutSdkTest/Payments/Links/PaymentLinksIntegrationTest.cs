using Checkout.Common;
using Checkout.Payments.Hosted;
using Checkout.Payments.Request;
using Checkout.Payments.Sender;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Product = Checkout.Common.Product;

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
                Currency = Currency.GBP,
                Billing = new BillingInformation { Address = GetAddress() },
                PaymentType = PaymentType.Regular,
                PaymentIp = "192.168.0.1",
                BillingDescriptor = new BillingDescriptor { Name = "name", City = "London", Reference = "reference" },
                Reference = "Reference",
                Description = "Description",
                DisplayName = "DisplayName",
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                AmountAllocations = new List<AmountAllocations>
                {
                    new AmountAllocations
                    {
                        Id = "ent_sdioy6bajpzxyl3utftdp7legq",
                        Amount = 100,
                        Reference = Guid.NewGuid().ToString(),
                        Commission = new Commission { Amount = 1, Percentage = 0.1 }
                    }
                },
                ExpiresIn = 1,
                Customer = new CustomerRequest { Email = GenerateRandomEmail(), Name = "name" },
                Shipping = new ShippingDetails { Address = GetAddress(), Phone = GetPhone() },
                Recipient = new PaymentRecipient
                {
                    AccountNumber = "1234567",
                    Country = CountryCode.ES,
                    DateOfBirth = "1985-05-15",
                    FirstName = "IT",
                    LastName = "TESTING",
                    Zip = "12345"
                },
                Processing = new ProcessingSettings { Aft = true },
                AllowPaymentMethods =
                    new List<PaymentSourceType> { PaymentSourceType.Card, PaymentSourceType.Ideal },
                DisabledPaymentMethods = 
                    new List<PaymentSourceType> { PaymentSourceType.EPS, PaymentSourceType.Ideal, PaymentSourceType.KNet },
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Gold Necklace",
                        Quantity = 1,
                        Price = 10,
                        Reference = "some description about item",
                    }
                },
                Metadata = new Dictionary<string, object>
                {
                    {"VoucherCode", "loyalty_10"},
                    {"discountApplied", "10"},
                    {"customer_id", "2190EF321"},
                },
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
                Risk = new RiskRequest
                {
                    Enabled = false
                },
                CustomerRetry = new PaymentRetryRequest
                {
                    MaxAttempts = 2
                },
                Sender = new PaymentInstrumentSender
                {
                    Reference = "8285282045818"
                },
                Locale = LocaleType.EnGb,
                Capture = true,
                CaptureOn = DateTime.Now,
                Instruction = new PaymentInstruction
                {
                    Purpose = PaymentPurposeType.Pension
                },
                PaymentMethodConfiguration = new PaymentMethodConfiguration
                {
                    Applepay = new Applepay
                    {
                        AccountHolder = new AccountHolder
                        {
                            FirstName = "John",
                            LastName = "Jones",
                            Type = AccountHolderType.Individual
                        }
                    },
                    Card = new Card
                    {
                        AccountHolder = new AccountHolder
                        {
                            FirstName = "John",
                            LastName = "Jones",
                            Type = AccountHolderType.Individual
                        }
                    },
                    Googlepay = new Googlepay
                    {
                        AccountHolder = new AccountHolder
                        {
                            FirstName = "John",
                            LastName = "Jones",
                            Type = AccountHolderType.Individual
                        }
                    }
                }
            };

            var response = await DefaultApi.PaymentLinksClient().Create(paymentLinkRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
            response.Links.Count.ShouldBe(2);
            response.Reference.ShouldNotBeNull();

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
            responseGet.Metadata.Count.ShouldBe(3);
            responseGet.Locale.ShouldBe(LocaleType.EnGb);
        }
    }
}