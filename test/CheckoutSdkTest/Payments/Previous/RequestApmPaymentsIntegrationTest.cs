using Checkout.Common;
using Checkout.Payments.Previous.Request;
using Checkout.Payments.Previous.Request.Source.Apm;
using Checkout.Payments.Previous.Response.Source;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Previous
{
    public class RequestApmPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldMakeAliPayPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestAlipaySource(),
                Currency = Currency.USD,
                Amount = 100,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Alipay);
        }
        
        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeBenefitPayPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestBenefitPaySource
                {
                    IntegrationType = "mobile"
                },
                Currency = Currency.USD,
                Amount = 100,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.BenefitPay);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeBoletoPayment_Redirect()
        {
            var boletoSource = new RequestBoletoSource
            {
                Country = CountryCode.BR,
                Description = "boleto payment",
                IntegrationType = IntegrationType.Redirect,
                Payer = new Payer
                {
                    Email = "bruce@wayne-enterprises.com",
                    Name = "Bruce Wayne",
                    Document = "53033315550"
                }
            };

            var paymentRequest = new PaymentRequest
            {
                Source = boletoSource,
                Currency = Currency.BRL,
                Amount = 100
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Source.Type().ShouldBe(PaymentSourceType.Boleto);
            payment.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Boleto);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeBoletoPayment_Direct()
        {
            var boletoSource = new RequestBoletoSource
            {
                Country = CountryCode.BR,
                Description = "boleto payment",
                IntegrationType = IntegrationType.Direct,
                Payer = new Payer
                {
                    Email = "bruce@wayne-enterprises.com",
                    Name = "Bruce Wayne",
                    Document = "53033315550"
                }
            };

            var paymentRequest = new PaymentRequest
            {
                Source = boletoSource,
                Currency = Currency.BRL,
                Amount = 100
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Declined);
            paymentResponse.ResponseSummary.ShouldBe("Rejected");
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["actions"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Declined);
            payment.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["actions"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Boleto);
        }
        
        [Fact]
        private async Task ShouldMakeEpsPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestEpsSource
                {
                    Purpose = "Mens black t-shirt L",
                },
                Currency = Currency.EUR,
                Amount = 100,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.EPS);
        }

        [Fact]
        private async Task ShouldMakeFawryPayment()
        {
            var fawrySource = new RequestFawrySource
            {
                Description = "Fawry Demo Payment",
                CustomerEmail = "bruce@wayne-enterprises.com",
                CustomerMobile = "01058375055",
                Products = new List<FawryProduct>
                {
                    new FawryProduct
                    {
                        ProductId = "0123456789",
                        Description = "Fawry Demo Product",
                        Price = 1000,
                        Quantity = 1
                    }
                }
            };

            var paymentRequest = new PaymentRequest
            {
                Source = fawrySource,
                Currency = Currency.EGP,
                Amount = 1000,
                Capture = true
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["approve"].ShouldNotBeNull();
            paymentResponse.Links["cancel"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["approve"].ShouldNotBeNull();
            payment.Links["cancel"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Fawry);
        }

        [Fact]
        private async Task ShouldMakeGiropayPayment()
        {
            var giropaySource = new RequestGiropaySource
            {
                Purpose = "CKO Giropay test",
            };

            var paymentRequest = new PaymentRequest
            {
                Source = giropaySource,
                Currency = Currency.EUR,
                Amount = 1000,
                Capture = true
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Giropay);
        }

        [Fact]
        private async Task ShouldMakeIdealPayment()
        {
            var idealSource = new RequestIdealSource
            {
                Bic = "INGBNL2A",
                Description = "ORD50234E89",
                Language = "nl"
            };

            var paymentRequest = new PaymentRequest
            {
                Source = idealSource,
                Currency = Currency.EUR,
                Amount = 1000,
                Capture = true
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Ideal);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeOxxoPayment()
        {
            var oxxoSource = new RequestOxxoSource
            {
                Country = CountryCode.MX,
                Description = "ORD50234E89",
                Payer = new Payer
                {
                    Name = "Bruce Wayne",
                    Email = "bruce@wayne-enterprises.com",
                }
            };

            var paymentRequest = new PaymentRequest
            {
                Source = oxxoSource,
                Currency = Currency.MXN,
                Amount = 100000,
                Capture = true
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Oxxo);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldMakePagoFacilPayment()
        {
            var pagoFacilSource = new RequestPagoFacilSource
            {
                Country = CountryCode.AR,
                Description = "simulate Via Pago Facil Demo Payment",
                Payer = new Payer
                {
                    Name = "Bruce Wayne",
                    Email = "bruce@wayne-enterprises.com",
                }
            };

            var paymentRequest = new PaymentRequest
            {
                Source = pagoFacilSource,
                Currency = Currency.ARS,
                Amount = 100000,
                Capture = true
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.PagoFacil);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeRapiPagoPayment()
        {
            var rapiPagoSource = new RequestRapiPagoSource
            {
                Country = CountryCode.AR,
                Description = "simulate Via Pago Facil Demo Payment",
                Payer = new Payer
                {
                    Name = "Bruce Wayne",
                    Email = "bruce@wayne-enterprises.com",
                }
            };

            var paymentRequest = new PaymentRequest
            {
                Source = rapiPagoSource,
                Currency = Currency.ARS,
                Amount = 100000,
                Capture = true
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.RapiPago);
        }

        [Fact]
        private async Task ShouldMakeSofortPayment()
        {
            var sofortSource = new RequestSofortSource
            {
                CountryCode = CountryCode.FR,
                LanguageCode = "fr"
            };

            var paymentRequest = new PaymentRequest
            {
                Source = sofortSource,
                Currency = Currency.EUR,
                Amount = 100,
                Capture = true
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Sofort);
        }
        
        [Fact]
        private async Task ShouldMakeKnetPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestKnetSource
                {
                    Language = "en",
                },
                Currency = Currency.KWD,
                Amount = 100,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse)payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.KNet);
        }
        
        [Fact]
        private async Task ShouldMakePrzelewy24Payment()
        {
            var paymentRequest = new PaymentRequest
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
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse)payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Przelewy24);
        }
        
        [Fact]
        private async Task ShouldMakePayPalPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestPayPalSource
                {
                    InvoiceNumber = "CKO00001",
                    LogoUrl = "https://www.example.com/logo.jpg",
                },
                Currency = Currency.EUR,
                Amount = 100,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse)payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.PayPal);
        }
        
        [Fact]
        private async Task ShouldMakePoliPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestPoliSource(),
                Currency = Currency.AUD,
                Amount = 100,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Declined);
            paymentResponse.Links["self"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Declined);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse)payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Poli);
        }
        
        [Fact]
        private async Task ShouldMakeBancontactPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestBancontactSource
                {
                    PaymentCountry = CountryCode.BE,
                    AccountHolderName = "Bruce Wayne",
                    BillingDescriptor = "CKO Demo - bancontact",
                },
                Currency = Currency.EUR,
                Amount = 100,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse)payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Bancontact);
        }
        
        [Fact]
        private async Task ShouldMakeQPayPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestQPaySource
                {
                    Quantity = 1,
                    Description = "QPay Demo Payment",
                    Language = "en",
                    NationalId = "070AYY010BU234M"
                },
                Currency = Currency.QAR,
                Amount = 100,
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse)payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.QPay);
        }
        
        [Fact(Skip = "unavailable")]
        private async Task ShouldMakeMultiBancoPayment()
        {
            var paymentRequest = new PaymentRequest
            {
                Source = new RequestMultiBancoSource
                {
                    PaymentCountry = CountryCode.PT,
                    AccountHolderName = "Bruce Wayne",
                    BillingDescriptor = "Multibanco Demo Payment"
                },
                Currency = Currency.EUR,
                Amount = 100,
                Capture = true
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await PreviousApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Multibanco);
        }
    }
}