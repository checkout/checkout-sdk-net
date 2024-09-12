using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Source.Apm;
using Checkout.Payments.Response.Source;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Product = Checkout.Payments.Request.Product;

namespace Checkout.Payments
{
    public class RequestApmPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldMakeAliPayPayment()
        {
            var source = RequestAlipayPlusSource.RequestAlipayPlusCnSource();
            source = RequestAlipayPlusSource.RequestAlipayPlusGCashSource();
            source = RequestAlipayPlusSource.RequestAlipayPlusHkSource();
            source = RequestAlipayPlusSource.RequestAlipayPlusDanaSource();
            source = RequestAlipayPlusSource.RequestAlipayPlusKakaoPaySource();
            source = RequestAlipayPlusSource.RequestAlipayPlusTrueMoneySource();
            source = RequestAlipayPlusSource.RequestAlipayPlusTngSource();
            source = RequestAlipayPlusSource.RequestAliPayPlusSource();

            var request = new PaymentRequest
            {
                Source = source,
                Amount = 10L,
                Currency = Currency.EUR,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
            };

            try
            {
                await DefaultApi.PaymentsClient().RequestPayment(request);
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo<CheckoutApiException>();
            }
        }

        [Fact]
        private async Task ShouldMakeIdealPayment()
        {
            var idealSource = new RequestIdealSource { Description = "ORD50234E89", Language = "nl" };

            var paymentRequest = new PaymentRequest
            {
                Source = idealSource,
                Reference = "REFERENCE",
                Currency = Currency.EUR,
                Amount = 1000,
                Capture = true,
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldNotBeNull();
            paymentResponse.Reference.ShouldBe("REFERENCE");
            paymentResponse.Links.ShouldNotBeNull();

            var payment = await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldNotBeNull();
            payment.Links.ShouldNotBeNull();

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

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

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
            ICheckoutApi previewApi = CheckoutSdk.Builder()
                .OAuth()
                .ClientCredentials(
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_PREVIEW_OAUTH_CLIENT_ID"),
                    System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_PREVIEW_OAUTH_CLIENT_SECRET"))
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
                Processing = new ProcessingSettings { TaxAmount = 500, ShippingAmount = 1000 },
                ProcessingChannelId = "pc_zs5fqhybzc2e3jmq3efvybybpq",
                Customer = new CustomerRequest
                {
                    Name = "Cecilia Chapman",
                    Email = "c.chapman@example.com",
                    Phone = new Phone { CountryCode = "+966", Number = "113 496 0000" }
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

        [Fact]
        private async Task ShouldMakeAfterPayPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestAfterPaySource { AccountHolder = new AccountHolder() },
                Amount = 10L,
                Currency = Currency.EUR,
                ProcessingChannelId = System.Environment.GetEnvironmentVariable("CHECKOUT_PROCESSING_CHANNEL_ID"),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
            };

            try
            {
                await DefaultApi.PaymentsClient().RequestPayment(request);
            }
            catch (Exception e)
            {
                e.ShouldBeAssignableTo<CheckoutApiException>();
            }
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeBenefitPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestBenefitSource(),
                Amount = 10L,
                Currency = Currency.BHD,
                Reference = "REFERENCE",
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact]
        private async Task ShouldMakeQPayPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestQPaySource
                {
                    Quantity = 1, Description = "QPay Demo Payment", Language = "en", NationalId = "070AYY010BU234M"
                },
                Amount = 10L,
                Currency = Currency.QAR,
                Reference = "REFERENCE",
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeMbwayPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestMbwaySource(),
                Amount = 10L,
                Currency = Currency.BHD,
                Reference = "REFERENCE",
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                "cko_processing_channel_id_invalid");
        }

        [Fact]
        private async Task ShouldMakeEpsPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestEpsSource { Purpose = "Mens black t-shirt L" },
                Amount = 10L,
                Currency = Currency.EUR,
                Reference = "REFERENCE",
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }
        
        [Fact]
        private async Task ShouldMakeIllicadoPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestIllicadoSource { 
                    BillingAddress = new Address
                    {
                        AddressLine1 = "Cecilia Chapman",
                        AddressLine2 = "711-2880 Nulla St.",
                        City = "Mankato",
                        State = "Mississippi",
                        Zip = "96522",
                        Country = CountryCode.SA
                    }
                },
                Amount = 10L,
                Currency = Currency.EUR,
                Reference = "REFERENCE",
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact(Skip = "Until it's fixed in Sandbox")]
        private async Task ShouldMakeGiropayPayment()
        {
            var accountHolder = new AccountHolder { FirstName = "Firstname", LastName = "Lastname" };
            
            var source = new RequestGiropaySource
            {
                AccountHolder = accountHolder,
            };

            var request = new PaymentRequest
            {
                Source = source,
                Amount = 10L,
                Currency = Currency.EUR,
                Reference = "REFERENCE",
                Description = "Description",
                Shipping = new ShippingDetails
                {
                    Address = GetAddress(),
                    Phone = GetPhone(),
                },
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure",
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact]
        private async Task ShouldMakePrzelewy24Payment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestP24Source
                {
                    PaymentCountry = CountryCode.PL,
                    AccountHolderName = "Bruce Wayne",
                    AccountHolderEmail = "bruce@wayne-enterprises.com",
                    BillingDescriptor = "P24 Demo Payment"
                },
                Currency = Currency.PLN,
                Amount = 100,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact]
        private async Task ShouldMakeKnetPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestKnetSource { Language = "en", },
                Currency = Currency.KWD,
                Amount = 100,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                ApmServiceUnavailable);
        }

        [Fact]
        private async Task ShouldMakeBancontactPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestBancontactSource
                {
                    PaymentCountry = CountryCode.BE,
                    AccountHolderName = "Bruce Wayne",
                    BillingDescriptor = "CKO Demo - bancontact",
                },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };
            
            var response = await DefaultApi.PaymentsClient().RequestPayment(request);
            
            response.Id.ShouldNotBeNull();
            response.Status.ShouldBe(PaymentStatus.Pending);
            response.Reference.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldMakeMultiBancoPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestMultiBancoSource
                {
                    PaymentCountry = CountryCode.PT,
                    AccountHolderName = "Bruce Wayne",
                    BillingDescriptor = "Multibanco Demo Payment"
                },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact]
        private async Task ShouldMakePostFinancePayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestPostFinanceSource
                {
                    PaymentCountry = CountryCode.CH,
                    AccountHolderName = "Bruce Wayne",
                    BillingDescriptor = "Multibanco Demo Payment"
                },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact]
        private async Task ShouldMakeStcPayPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestStcPaySource(),
                Currency = Currency.SAR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                Customer = new PaymentCustomerRequest
                {
                    Email = GenerateRandomEmail(), Name = "Louis Smith", Phone = GetPhone()
                },
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                "merchant_data_delegated_authentication_failed");
        }

        [Fact]
        private async Task ShouldMakeAlmaPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestAlmaSource { BillingAddress = GetAddress() },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact(Skip = "Unavailable")]
        private async Task ShouldMakeKlarnaPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestKlarnaSource { AccountHolder = GetAccountHolder() },
                Currency = Currency.QAR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                ApmServiceUnavailable);
        }

        [Fact(Skip = "Unavailable")]
        private async Task ShouldMakePayPalPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestPayPalSource
                {
                    plan = new BillingPlan
                    {
                        Type = BillingPlanType.ChannelInitiatedBillingSingleAgreement,
                        SkipShippingAddress = true,
                        ImmutableShippingAddress = false
                    }
                },
                Items = new List<Product> { new Product { Name = "Laptop", Quantity = 1, UnitPrice = 10, } },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };
            
            var response = await DefaultApi.PaymentsClient().RequestPayment(request);
            
            response.Id.ShouldNotBeNull();
            response.Status.ShouldBe(PaymentStatus.Pending);
            response.Reference.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldMakeFawryPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestFawrySource
                {
                    Description = "Fawry Demo Payment",
                    CustomerMobile = "01058375055",
                    CustomerEmail = "bruce@wayne-enterprises.com",
                    Products = new FawryProduct[]
                    {
                        new FawryProduct
                        {
                            ProductId = "0123456789",
                            Quantity = 1,
                            Price = 10,
                            Description = "Fawry Demo Product"
                        }
                    }
                },
                Currency = Currency.EGP,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact]
        private async Task ShouldMakeTrustlyPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestTrustlySource { BillingAddress = GetAddress() },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }

        [Fact]
        private async Task ShouldMakeCvConnectPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestCvConnectSource { BillingAddress = GetAddress() },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                PayeeNotOnboarded);
        }
        
        [Fact]
        private async Task ShouldMakeSepaPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestSepaSource()
                {
                    Country = CountryCode.ES,
                    Currency = Currency.EUR,
                    AccountNumber = "HU93116000060000000012345676",
                    BankCode = "37040044",
                    MandateId = "man_12321233211",
                    DateOfSignature = ("2023-01-01"),
                    AccountHolder = GetAccountHolder()
                },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
            };

            await CheckErrorItem(async () => await DefaultApi.PaymentsClient().RequestPayment(request),
                ApmServiceUnavailable);
        }
    }
}