using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Payments
{
    public class AlternativePaymentsSourcePaymentsTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public AlternativePaymentsSourcePaymentsTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        public static IEnumerable<object[]> PaymentMethodsTestData =>
            new List<object[]>
            {
                new object[] { new AlternativePaymentSource() {Type = "giropay", Bic = "TESTDETT421", Purpose = "CKO BaseSource Test" } },
                new object[] { new GiropayRequestSource(bic: "TESTDETT421", purpose: "CKO GiropaySource Test") },
                new object[] { new IdealRequestSource(issuer_id: "INGBNL2A") }
            };

        [Theory]
        [MemberData(nameof(PaymentMethodsTestData))]
        public async Task RequestAlternativePaymentMethodPayment(IAlternativePaymentRequestSource alternativePaymentMethodRequestSource)
        {
            PaymentRequest<IAlternativePaymentRequestSource> paymentRequest = TestHelper.CreateAlternativePaymentMethodRequest(alternativePaymentMethodRequestSource, currency: Currency.EUR);
            paymentRequest.ThreeDS = false;
            
            PaymentResponse apiResponse = await _api.Payments.RequestAsync(paymentRequest);

            apiResponse.IsPending.ShouldBeTrue();
            apiResponse.Pending.ShouldNotBeNull();
            apiResponse.Pending.Id.ShouldNotBeNullOrEmpty();
            apiResponse.Pending.Status.ShouldBe(PaymentStatus.Pending);
            apiResponse.Pending.Reference.ShouldBe(paymentRequest.Reference);
            apiResponse.Pending.Customer.ShouldNotBeNull();
            apiResponse.Pending.Customer.Id.ShouldNotBeNullOrEmpty();
            apiResponse.Pending.Customer.Email.ShouldNotBeNullOrEmpty();
            apiResponse.Pending.HasLink("redirect").ShouldBeTrue();
        }
    }
}
