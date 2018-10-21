using System;
using Checkout.Common;
using Checkout.Payments;
using Checkout.Tests.Mocks;

namespace Checkout.Tests
{
    public static class TestHelper
    {
        public static PaymentRequest<CardSource> CreateCardPaymentRequest(int? amount = 100)
        {
            return new PaymentRequest<CardSource>(
                new CardSource(TestCardSource.Visa.Number, TestCardSource.Visa.ExpiryMonth, TestCardSource.Visa.ExpiryYear)
                {
                    Cvv = TestCardSource.Visa.Cvv
                },
                Currency.GBP,
                amount
            )
            {
                Capture = false,
                Customer = new CustomerRequest() { Email = TestHelper.GenerateRandomEmail()},
                Reference = Guid.NewGuid().ToString()
            };
        }
        public static PaymentRequest<IAlternativePaymentRequestSource> CreateAlternativePaymentMethodRequest(IAlternativePaymentRequestSource alternativePaymentMethodRequestSource, int? amount = 100, string currency = Currency.GBP)
        {
            return new PaymentRequest<IAlternativePaymentRequestSource>(
                alternativePaymentMethodRequestSource,
                currency,
                amount
            )
            {
                Capture = false,
                Customer = new CustomerRequest() { Email = TestHelper.GenerateRandomEmail() },
                Reference = Guid.NewGuid().ToString()
            };
        }
        public static string GenerateRandomEmail()
        {
            return Guid.NewGuid().ToString("n") + "@checkout-sdk-net.com";
        }
    }
}