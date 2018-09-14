using Shouldly;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Payments
{
    public class CustomerSourceTests : IClassFixture<ApiTestFixture>
    {
        public CustomerSourceTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            Api = fixture.Api;
        }

        public ICheckoutApi Api { get; private set; }

        [Fact]
        public async Task CanRequestCardPayment()
        {
            PaymentRequest<CardSource> firstCardPayment = TestHelper.CreateCardPaymentRequest();
            PaymentResponse firstCardPaymentResponse = await Api.Payments.RequestAsync(firstCardPayment);
            CustomerSource customerSource = new CustomerSource(firstCardPayment.Customer.Id, firstCardPayment.Customer.Email);
            PaymentRequest<CustomerSource> customerPaymentRequest = new PaymentRequest<CustomerSource>(
                customerSource,
                Currency.GBP,
                100
            )
            {
                Capture = false,
            };

            PaymentResponse apiResponseForCustomerSourcePayment = await Api.Payments.RequestAsync(customerPaymentRequest);

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
            apiResponseForCustomerSourcePayment.Payment.Source.AsCardSourceResponse().Id.ShouldBe(firstCardPaymentResponse.Payment?.Source?.AsCardSourceResponse().Id);
            apiResponseForCustomerSourcePayment.Payment.CanCapture().ShouldBeTrue();
            apiResponseForCustomerSourcePayment.Payment.CanVoid().ShouldBeTrue();
        }
    }
}