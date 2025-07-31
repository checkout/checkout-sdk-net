using Checkout.Authentication.Standalone.Common.MerchantRiskInfo;
using Checkout.Common;
using Checkout.Payments.Request.Source.Contexts;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsIntegrationTest : SandboxTestFixture
    {
        public PaymentContextsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        private async Task ShouldMakeAPayPalPaymentContextRequest()
        {
            var paymentContextsRequest = new PaymentContextsRequest
            {
                Source = new PaymentContextsPayPalSource(),
                Amount = 1000,
                Currency = Currency.EUR,
                PaymentType = PaymentType.Regular,
                Capture = true,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/fail",
                Items = new List<PaymentContextsItems>
                {
                    new PaymentContextsItems { Name = "mask", Quantity = 1, UnitPrice = 1000, TotalAmount = 1000 }
                },
            };

            var response = await DefaultApi.PaymentContextsClient().RequestPaymentContexts(paymentContextsRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.PartnerMetadata.OrderId.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldMakeAKlarnaPaymentContextRequest()
        {
            var source = new PaymentContextsKlarnaSource
            {
                AccountHolder = new AccountHolder { BillingAddress = new Address { Country = CountryCode.DE } }
            };

            var paymentContextsRequest = new PaymentContextsRequest
            {
                Source = source,
                Amount = 1000,
                Currency = Currency.EUR,
                PaymentType = PaymentType.Regular,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                Items = new List<PaymentContextsItems>
                {
                    new PaymentContextsItems
                    {
                        Name = "mask",
                        Quantity = 1,
                        UnitPrice = 1000,
                        TotalAmount = 1000,
                        Reference = "BA67A"
                    }
                },
                Processing = new PaymentContextsProcessing { Locale = "en-GB" }
            };

            var response = await DefaultApi.PaymentContextsClient().RequestPaymentContexts(paymentContextsRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.PartnerMetadata.SessionId.ShouldNotBeNull();
            response.PartnerMetadata.ClientToken.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
        }
        
        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeAStcpayPaymentContextRequest()
        {
            var paymentContextsRequest = new PaymentContextsRequest
            {
                Source = new PaymentContextsStcpaySource(),
                Amount = 1000,
                Currency = Currency.EUR,
                PaymentType = PaymentType.Regular,
                Capture = true,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/fail",
                Items = new List<PaymentContextsItems>
                {
                    new PaymentContextsItems { Name = "mask", Quantity = 1, UnitPrice = 1000, TotalAmount = 1000 }
                },
            };
            
            var response = await DefaultApi.PaymentContextsClient().RequestPaymentContexts(paymentContextsRequest);

            response.ShouldNotBeNull();
        }
        
        [Fact]
        private async Task ShouldMakeATabbyPaymentContextRequest()
        {
            var paymentContextsRequest = new PaymentContextsRequest
            {
                Source = new PaymentContextsTabbySource(),
                Amount = 1000,
                Currency = Currency.EUR,
                PaymentType = PaymentType.Regular,
                Capture = true,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/fail",
                Items = new List<PaymentContextsItems>
                {
                    new PaymentContextsItems { Name = "mask", Quantity = 1, UnitPrice = 1000, TotalAmount = 1000 }
                },
            };

            await CheckErrorItem(
                async () => await DefaultApi.PaymentContextsClient().RequestPaymentContexts(paymentContextsRequest),
                "currency_not_supported");
        }

        [Fact]
        private async Task ShouldGetAPaymentContext()
        {
            var paymentContextsRequest = new PaymentContextsRequest
            {
                Source = new PaymentContextsPayPalSource(),
                Amount = 2000,
                Currency = Currency.USD,
                PaymentType = PaymentType.Regular,
                Customer = new PaymentContextsCustomerRequest
                {
                    Email = GenerateRandomEmail(),
                    EmailVerified = true,
                    Name = "John Smith",
                },
                Capture = true,
                Shipping = new ShippingDetails
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "sb-z5klz21518170@personal.example.com",
                    Address = GetAddress(),
                    Phone = GetPhone(),
                    FromAddressZip = "43434",
                    Timeframe = DeliveryTimeframeType.ElectronicDelivery,
                    Method = PaymentContextsShippingMethod.Digital,
                    Delay = 0
                },
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/fail",
                Items = new List<PaymentContextsItems>
                {
                    new PaymentContextsItems { Name = "mask", Quantity = 1, UnitPrice = 2000 }
                }
            };

            var paymentContextResponse =
                await DefaultApi.PaymentContextsClient().RequestPaymentContexts(paymentContextsRequest);

            var response = await DefaultApi.PaymentContextsClient().GetPaymentContextDetails(paymentContextResponse.Id);

            response.ShouldNotBeNull();
            response.PaymentRequest.ShouldNotBeNull();
            response.PaymentRequest.Amount.ShouldBe(2000);
            response.PaymentRequest.Currency.ShouldBe(Currency.USD);
            response.PaymentRequest.PaymentType.ShouldBe(PaymentType.Regular);
            response.PaymentRequest.AuthorizationType.ShouldBe("Final");
            response.PaymentRequest.Capture.ShouldBe(true);
            response.PaymentRequest.Items[0].Name.ShouldBe("mask");
            response.PaymentRequest.Items[0].Quantity.ShouldBe(1);
            response.PaymentRequest.Items[0].UnitPrice.ShouldBe(2000);
            response.PaymentRequest.SuccessUrl.ShouldBe("https://example.com/payments/success");
            response.PaymentRequest.FailureUrl.ShouldBe("https://example.com/payments/fail");
            response.PartnerMetadata.ShouldNotBeNull();
            response.PartnerMetadata.OrderId.ShouldNotBeNull();
        }
    }
}