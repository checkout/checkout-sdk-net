using System;
using Checkout.Sdk.Common;
using Checkout.Sdk.Payments;
using Checkout.Sdk.Tests.Mocks;

namespace Checkout.Sdk.Tests
{
    public static class TestHelper
    {
        public static PaymentRequest<CardSource> CreateCardPaymentRequest()
        {
            return new PaymentRequest<CardSource>(
                new CardSource(TestCardSource.Visa.Number, TestCardSource.Visa.ExpiryMonth, TestCardSource.Visa.ExpiryYear),
                Currency.GBP,
                100
            )
            {
                Capture = false,
                Customer = new CustomerSource(null, TestHelper.GenerateRandomEmail()),
                Reference = Guid.NewGuid().ToString()
            };
        }
        public static string GenerateRandomEmail()
        {
            return Guid.NewGuid().ToString("n") + "@checkout-sdk-net.com";
        }
    }
}