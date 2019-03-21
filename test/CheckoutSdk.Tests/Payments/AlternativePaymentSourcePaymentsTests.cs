using Shouldly;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace Checkout.Tests.Payments
{
    public class AlternativePaymentSourcePaymentsTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public AlternativePaymentSourcePaymentsTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanRequestGiropayPayment()
        {
            var alternativePaymentSource = new AlternativePaymentSource("giropay") 
            {
                { "bic", "TESTDETT421" }, 
                { "purpose", "CKO giropay test" }
            };

            await RequestAlternativePaymentAsync(alternativePaymentSource);
        }

        [Fact]
        public async Task CanRequestIdealPayment()
        {
            var alternativePaymentSource = new AlternativePaymentSource("ideal") 
            {
                { "bic", "INGBNL2A" },
                { "description", "NET SDK Test" }
            };
            
            await RequestAlternativePaymentAsync(alternativePaymentSource);
        }

        [Fact]
        public async Task CanGetAlternativePayment()
        {
            var alternativePaymentSource = new AlternativePaymentSource("giropay") 
            { 
                { "bic", "TESTDETT421" }, 
                { "purpose", "CKO giropay test" } 
            };
            
            PaymentPending payment = await RequestAlternativePaymentAsync(alternativePaymentSource);

            GetPaymentResponse verifiedPayment = await _api.Payments.GetAsync(payment.Id);

            verifiedPayment.ShouldNotBeNull();
            verifiedPayment.Id.ShouldBe(payment.Id);

            var verifiedSource = verifiedPayment.Source.AsAlternativePayment();
            foreach (string key in verifiedSource.Keys)
            {
               verifiedSource[key].ShouldBe(alternativePaymentSource[key]);
            }
        }

        private async Task<PaymentPending> RequestAlternativePaymentAsync(AlternativePaymentSource alternativePaymentSource)
        {
            PaymentRequest<IRequestSource> paymentRequest = TestHelper.CreateAlternativePaymentMethodRequest(alternativePaymentSource, currency: Currency.EUR);

            PaymentResponse apiResponse = await _api.Payments.RequestAsync(paymentRequest);
            apiResponse.IsPending.ShouldBeTrue();
            apiResponse.Pending.ShouldNotBeNull();

            PaymentPending pendingPayment = apiResponse.Pending;
            pendingPayment.Id.ShouldNotBeNullOrEmpty();
            pendingPayment.Status.ShouldBe(PaymentStatus.Pending);
            pendingPayment.Reference.ShouldBe(paymentRequest.Reference);
            pendingPayment.Customer.ShouldNotBeNull();
            pendingPayment.Customer.Id.ShouldNotBeNullOrEmpty();
            pendingPayment.Customer.Email.ShouldNotBeNullOrEmpty();
            pendingPayment.RequiresRedirect().ShouldBeTrue();
            pendingPayment.GetRedirectLink().ShouldNotBeNull();

            return pendingPayment;
        }
    }
}
