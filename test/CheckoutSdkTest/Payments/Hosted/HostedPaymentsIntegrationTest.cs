using Checkout.Common;
using Checkout.Instruments.Previous;
using Checkout.Payments.Request;
using Checkout.Payments.Sender;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Product = Checkout.Common.Product;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentsIntegrationTest : SandboxTestFixture
    {
        public HostedPaymentsIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldCreateAndGetHostedPayment()
        {
            var hostedPaymentRequest = CreateHostedPaymentRequest();

            var createResponse = await DefaultApi.HostedPaymentsClient().CreateHostedPaymentsPageSession(hostedPaymentRequest);

            createResponse.ShouldNotBeNull();
            createResponse.Id.ShouldNotBeNullOrEmpty();
            createResponse.Reference.ShouldNotBeNullOrEmpty();
            createResponse.Links.ShouldNotBeNull();
            createResponse.Links.ContainsKey("redirect").ShouldBeTrue();

            var getResponse = await DefaultApi.HostedPaymentsClient().GetHostedPaymentsPageDetails(createResponse.Id);

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
            return new HostedPaymentRequest
            {
                Amount = 1000L,
                Currency = Currency.GBP,
                PaymentType = PaymentType.Regular,
                PaymentIp = "192.168.0.1",
                BillingDescriptor = new BillingDescriptor
                {
                    Name = "The Jewelry Shop",
                    City = "London",
                    Reference = "ORD-123A"
                },
                Reference = "reference",
                Description = "Payment for Gold Necklace",
                DisplayName = "The Jewelry Shop",
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                AmountAllocations = new List<AmountAllocations>
                {
                    new AmountAllocations
                    {
                        Id = "ent_w4jelhppmfiufdnatam37wrfc4",
                        Amount = 1000L,
                        Reference = "ORD-5023-4E89",
                        Commission = new Commission
                        {
                            Amount = 1000L,
                            Percentage = 1.125
                        }
                    }
                },
                Customer = new InstrumentCustomerRequest
                {
                    Email = "brucewayne@email.com",
                    Name = "Bruce Wayne"
                },
                Shipping = new ShippingDetails
                {
                    Address = GetAddress(),
                    Phone = GetPhone()
                },
                Billing = new BillingInformation
                {
                    Address = GetAddress(),
                    Phone = GetPhone()
                },
                Recipient = new PaymentRecipient
                {
                    DateOfBirth = "1985-05-15",
                    AccountNumber = "5555554444",
                    Address = GetAddress(),
                    Zip = "SW1A",
                    FirstName = "John",
                    LastName = "Jones"
                },
                Processing = new ProcessingSettings
                {
                    Aft = true
                },
                AllowPaymentMethods = new List<PaymentSourceType>
                {
                    PaymentSourceType.Card,
                    PaymentSourceType.Googlepay,
                    PaymentSourceType.Applepay
                },
                DisabledPaymentMethods = new List<PaymentSourceType>
                {
                    PaymentSourceType.EPS,
                    PaymentSourceType.Ideal,
                    PaymentSourceType.Knet
                },
                Products = new List<Product>
                {
                    new Product
                    {
                        Reference = "string",
                        Name = "Gold Necklace",
                        Quantity = 1L,
                        Price = 1000L
                    }
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
                SuccessUrl = "https://example.com/payments/success",
                CancelUrl = "https://example.com/payments/cancel",
                FailureUrl = "https://example.com/payments/failure",
                Locale = LocaleType.Ar,
                ThreeDs = new ThreeDsRequest
                {
                    Enabled = false,
                    AttemptN3D = false,
                    ChallengeIndicator = ChallengeIndicatorType.NoPreference,
                    AllowUpgrade = true,
                    Exemption = Exemption.LowValue
                },
                Capture = true,
                CaptureOn = DateTime.UtcNow,
                Instruction = new PaymentInstruction
                {
                    Purpose = PaymentPurposeType.Donations
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
        }
    }
}