using Shouldly;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Payments
{
    public class CustomerSourcePaymentsTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public CustomerSourcePaymentsTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanRequestCardPayment()
        {
            var firstCardPayment = TestHelper.CreateCardPaymentRequest();
            var firstCardPaymentResponse = await _api.Payments.RequestAsync(firstCardPayment);
            var customerSource = new CustomerSource(firstCardPayment.Customer.Id, firstCardPayment.Customer.Email);
            var customerPaymentRequest = new PaymentRequest<CustomerSource>(
                customerSource,
                Currency.GBP,
                100
            )
            {
                Capture = false,
            };

            PaymentResponse paymentResponse = await _api.Payments.RequestAsync(customerPaymentRequest);

            paymentResponse.Payment.ShouldNotBeNull();
            paymentResponse.Payment.Approved.ShouldBeTrue();
            paymentResponse.Payment.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Id.ShouldNotBe(firstCardPaymentResponse.Payment.Id);
            paymentResponse.Payment.ActionId.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Amount.ShouldBe(customerPaymentRequest.Amount.Value);
            paymentResponse.Payment.Currency.ShouldBe(customerPaymentRequest.Currency);
            paymentResponse.Payment.Reference.ShouldBe(customerPaymentRequest.Reference);
            paymentResponse.Payment.Customer.ShouldNotBeNull();
            paymentResponse.Payment.Customer.Id.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Customer.Email.ShouldNotBeNullOrEmpty();
            paymentResponse.Payment.Customer.Id.ShouldBe(firstCardPaymentResponse.Payment?.Customer?.Id);
            paymentResponse.Payment.Source.AsCard().ShouldNotBeNull();
            paymentResponse.Payment.Source.AsCard().Id.ShouldBe(firstCardPaymentResponse.Payment?.Source?.AsCard().Id);
            paymentResponse.Payment.CanCapture().ShouldBeTrue();
            paymentResponse.Payment.CanVoid().ShouldBeTrue();
        }
    }
}