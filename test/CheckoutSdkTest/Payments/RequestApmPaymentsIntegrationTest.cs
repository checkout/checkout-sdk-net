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
                ProcessingChannelId = "pc_5jp2az55l3cuths25t5p3xhwru",
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

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldNotBeNull();
            paymentResponse.ResponseSummary.ShouldNotBeNull();
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

        [Fact]
        private async Task ShouldMakeAfterPayPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestAfterPaySource {AccountHolder = new AccountHolder()},
                Amount = 10L,
                Currency = Currency.EUR,
                ProcessingChannelId = "pc_5jp2az55l3cuths25t5p3xhwru",
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
        private async Task ShouldMakeQPayPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestQPaySource(),
                Amount = 10L,
                Currency = Currency.BHD,
                Reference = "REFERENCE",
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
        private async Task ShouldMakeEpsPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestEpsSource {Purpose = "Mens black t-shirt L"},
                Amount = 10L,
                Currency = Currency.BHD,
                Reference = "REFERENCE",
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
        private async Task ShouldMakeGiropayPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestGiropaySource {Purpose = "CKO Giropay test",},
                Amount = 10L,
                Currency = Currency.BHD,
                Reference = "REFERENCE",
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
        private async Task ShouldMakeKnetPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestKnetSource
                {
                    Language = "en",
                },
                Currency = Currency.KWD,
                Amount = 100,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
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
        private async Task ShouldMakeBancontactPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestBancontactSource
                {
                    Language = "en",
                },
                Currency = Currency.EUR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
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
        private async Task ShouldMakePostFinancePayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestPostFinanceSource
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
        private async Task ShouldMakeStcPayPayment()
        {
            var request = new PaymentRequest
            {
                Source = new RequestMultiBancoSource
                {
                    PaymentCountry = CountryCode.PT,
                    AccountHolderName = "Bruce Wayne",
                    BillingDescriptor = "Multibanco Demo Payment"
                },
                Currency = Currency.QAR,
                Amount = 10,
                Reference = Guid.NewGuid().ToString(),
                SuccessUrl = "https://testing.checkout.com/sucess",
                FailureUrl = "https://testing.checkout.com/failure"
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
    }
}