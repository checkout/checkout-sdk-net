using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Source.Apm;
using Checkout.Payments.Response.Source;
using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    public class RequestApmPaymentsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldMakeBalotoPayment()
        {
            var balotoSource = new RequestBalotoSource
            {
                Country = CountryCode.CO,
                Description = "simulate Via Baloto Demo Payment",
                Payer = new BalotoPayer
                {
                    Email = "bruce@wayne-enterprises.com",
                    Name = "Bruce Wayne"
                }
            };

            var paymentRequest = new PaymentRequest
            {
                Source = balotoSource,
                Currency = Currency.COP,
                Amount = 100000
            };

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();
            paymentResponse.Links["simulator:payment-succeed"].ShouldNotBeNull();
            paymentResponse.Links["simulator:payment-expire"].ShouldNotBeNull();

            var payment = await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

            payment.ShouldNotBeNull();

            payment.Status.ShouldBe(PaymentStatus.Pending);
            payment.Links["self"].ShouldNotBeNull();
            payment.Links["redirect"].ShouldNotBeNull();
            payment.Links["simulator:payment-succeed"].ShouldNotBeNull();
            payment.Links["simulator:payment-expire"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Baloto);
        }

        [Fact]
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

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["redirect"].ShouldNotBeNull();

            var payment = await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

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

        [Fact]
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

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Declined);
            paymentResponse.ResponseSummary.ShouldBe("Rejected");
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["actions"].ShouldNotBeNull();

            var payment = await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

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

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);

            paymentResponse.ShouldNotBeNull();
            paymentResponse.Status.ShouldBe(PaymentStatus.Pending);
            paymentResponse.ResponseSummary.ShouldBeNull();
            paymentResponse.Links["self"].ShouldNotBeNull();
            paymentResponse.Links["approve"].ShouldNotBeNull();
            paymentResponse.Links["cancel"].ShouldNotBeNull();

            var payment = await DefaultApi.PaymentsClient().GetPaymentDetails(paymentResponse.Id);

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
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Ideal);
        }

        [Fact]
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
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Oxxo);
        }

        [Fact]
        private async Task ShouldMakePagoFacilPayment()
        {
            var pagoFacilSource = new RequestPagoFacilSource()
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
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.PagoFacil);
        }

        [Fact]
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
            payment.Links["redirect"].ShouldNotBeNull();

            payment.Source.ShouldBeOfType(typeof(AlternativePaymentSourceResponse));
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.RapiPago);
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
                Capture = true
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
            var source = (AlternativePaymentSourceResponse) payment.Source;
            source.Count.ShouldBePositive();
            source.Type().ShouldBe(PaymentSourceType.Sofort);
        }
    }
}