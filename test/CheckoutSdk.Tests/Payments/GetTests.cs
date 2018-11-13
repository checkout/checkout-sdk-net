using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Payments
{
    public class GetTests : ApiTestFixture
    {
        [Fact]
        public async Task CanGetCardPayment()
        {
            PaymentProcessed payment = await MakeCardPaymentAsync();

            GetPaymentResponse verifiedPayment = await Api.Payments.GetAsync(payment.Id);

            verifiedPayment.ShouldNotBeNull();
            verifiedPayment.Id.ShouldBe(payment.Id);
            verifiedPayment.Source.AsCard().Id.ShouldBe(payment.Source.AsCard().Id);
        }

        public static IEnumerable<object[]> AlternativePaymentMethodsTestData =>
            new List<object[]>
            {
                new object[] { new AlternativePaymentSource("giropay") { { "bic", "TESTDETT421" }, { "purpose", "CKO giropay test" } }, Currency.EUR },
                new object[] { new AlternativePaymentSource("ideal") { { "issuer_id", "INGBNL2A" } }, Currency.EUR }
            };

        [Theory]
        [MemberData(nameof(AlternativePaymentMethodsTestData))]
        public async Task CanGetAlternativePayment(IRequestSource alternativePaymentMethodRequestSource, string currency)
        {
            PaymentPending payment = await MakeAlternativePaymentAsync(alternativePaymentMethodRequestSource, currency);

            GetPaymentResponse verifiedPayment = await Api.Payments.GetAsync(payment.Id);

            verifiedPayment.ShouldNotBeNull();
            verifiedPayment.Id.ShouldBe(payment.Id);
            if(verifiedPayment.Source.Type == "giropay")
            {
                (verifiedPayment.Source as Dictionary<string, string>)["bic"].ShouldBe((alternativePaymentMethodRequestSource as Dictionary<string, string>)["bic"]);
            }
        }

        async Task<PaymentProcessed> MakeCardPaymentAsync()
        {
            PaymentRequest<CardSource> paymentRequest = TestHelper.CreateCardPaymentRequest(amount: 1000);

            PaymentResponse paymentResponse = await Api.Payments.RequestAsync(paymentRequest);
            paymentResponse.Payment.CanCapture().ShouldBeTrue();

            return paymentResponse.Payment;
        }

        async Task<PaymentPending> MakeAlternativePaymentAsync(IRequestSource alternativePaymentMethodRequestSource, string currency)
        {
            PaymentRequest<IRequestSource> paymentRequest = TestHelper.CreateAlternativePaymentMethodRequest(alternativePaymentMethodRequestSource, currency: currency);

            PaymentResponse paymentResponse = await Api.Payments.RequestAsync(paymentRequest);
            paymentResponse.IsPending.ShouldBeTrue();

            return paymentResponse.Pending;
        }
    }
}