using Checkout.Common;
using Checkout.Payments.Four.Request;
using Checkout.Payments.Four.Request.Source.Apm;
using Checkout.Payments.Four.Response.Source;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Product = Checkout.Payments.Four.Request.Product;

namespace Checkout.Payments.Four
{
    public class RequestApmPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldMakeIdealPayment()
        {
            var idealSource = new RequestIdealSource {Bic = "INGBNL2A", Description = "ORD50234E89", Language = "nl"};

            var paymentRequest = new PaymentRequest
            {
                Source = idealSource,
                Currency = Currency.EUR,
                Amount = 1000,
                Capture = true,
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            var paymentResponse = await FourApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await FourApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse)payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Ideal);
        }

        [Fact]
        private async Task ShouldMakeSofortPayment()
        {
            var sofortSource = new RequestSofortSource();

            var paymentRequest = new PaymentRequest
            {
                Source = sofortSource,
                Currency = Currency.EUR,
                Amount = 100,
                Capture = true,
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            var paymentResponse = await FourApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await FourApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse)payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Sofort);
        }

        [Fact(Skip = "Preview")]
        private async Task ShouldMakeTamaraPayment()
        {
            Checkout.Four.ICheckoutApi previewApi = CheckoutSdk.FourSdk().OAuth()
                .ClientCredentials(System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_PREVIEW_OAUTH_CLIENT_ID"),
                    System.Environment.GetEnvironmentVariable("CHECKOUT_FOUR_PREVIEW_OAUTH_CLIENT_SECRET"))
                .Environment(Environment.Sandbox)
                .Build();

            var tamaraSource = new RequestTamaraSource();
            tamaraSource.BillingAddress = new Address
            {
                AddressLine1 = "Cecilia Chapman",
                AddressLine2 = "711-2880 Nulla St.",
                City = "Mankato",
                State = "Mississippi",
                Zip = "96522",
                Country = CountryCode.SA
            };

            var paymentRequest = new PaymentRequest
            {
                Source = tamaraSource,
                Currency = Currency.SAR,
                Amount = 10000,
                Capture = true,
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
                Reference = "ORD-5023-4E89",
                Processing = new ProcessingSettings {TaxAmount = 500, ShippingAmount = 1000},
                ProcessingChannelId = "pc_zs5fqhybzc2e3jmq3efvybybpq",
                Customer = new CustomerRequest
                {
                    Name = "Cecilia Chapman",
                    Email = "c.chapman@example.com",
                    Phone = new Phone {CountryCode = "+966", Number = "113 496 0000"}
                },
                Items = new List<Product>
                {
                    new Product
                    {
                        Name = "Item name",
                        Quantity = 3,
                        UnitPrice = 100,
                        TotalAmount = 100,
                        TaxAmount = 19,
                        DiscountAmount = 2,
                        Reference = "some description about item",
                        ImageUrl = "https://some_s3bucket.com",
                        Url = "https://some.website.com/item",
                        Sku = "123687000111"
                    }
                }
            };

            var paymentResponse = await previewApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Reference.ShouldNotBeNullOrEmpty();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links.ShouldNotBeEmpty();
            paymentResponse.Customer.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Customer.Name.ShouldNotBeEmpty();
            paymentResponse.Customer.Email.ShouldNotBeEmpty();
            paymentResponse.Customer.Phone.ShouldNotBeNull();
            paymentResponse.Processing.PartnerPaymentId.ShouldNotBeNull();
            paymentResponse.Links.ShouldNotBeEmpty();
        }
    }
}