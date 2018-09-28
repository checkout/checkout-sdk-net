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

            PaymentResponse apiResponseForCustomerSourcePayment = await _api.Payments.RequestAsync(customerPaymentRequest);

            apiResponseForCustomerSourcePayment.Payment.ShouldNotBeNull();
            apiResponseForCustomerSourcePayment.Payment.Approved.ShouldBeTrue();
            apiResponseForCustomerSourcePayment.Payment.Id.ShouldNotBeNullOrEmpty();
            apiResponseForCustomerSourcePayment.Payment.Id.ShouldNotBe(firstCardPaymentResponse.Payment.Id);
            apiResponseForCustomerSourcePayment.Payment.ActionId.ShouldNotBeNullOrEmpty();
            apiResponseForCustomerSourcePayment.Payment.Amount.ShouldBe(customerPaymentRequest.Amount.Value);
            apiResponseForCustomerSourcePayment.Payment.Currency.ShouldBe(customerPaymentRequest.Currency);
            apiResponseForCustomerSourcePayment.Payment.Reference.ShouldBe(customerPaymentRequest.Reference);
            apiResponseForCustomerSourcePayment.Payment.Customer.ShouldNotBeNull();
            apiResponseForCustomerSourcePayment.Payment.Customer.Id.ShouldNotBeNullOrEmpty();
            apiResponseForCustomerSourcePayment.Payment.Customer.Email.ShouldNotBeNullOrEmpty();
            apiResponseForCustomerSourcePayment.Payment.Customer.Id.ShouldBe(firstCardPaymentResponse.Payment?.Customer?.Id);
            apiResponseForCustomerSourcePayment.Payment.Source.AsCard().Id.ShouldBe(firstCardPaymentResponse.Payment?.Source?.AsCard().Id);
            apiResponseForCustomerSourcePayment.Payment.CanCapture().ShouldBeTrue();
            apiResponseForCustomerSourcePayment.Payment.CanVoid().ShouldBeTrue();
        }
    }
}