using System;
using Checkout.Common;
using Checkout.Payments;
using Checkout.Tokens;

namespace Checkout.Tests
{
    public static class TestHelper
    {
        public static PaymentRequest<CardSource> CreateCardPaymentRequest(long? amount = 100)
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
                Customer = new Checkout.Payments.CustomerRequest() { Email = TestHelper.GenerateRandomEmail()},
                Reference = Guid.NewGuid().ToString()
            };
        }

        public static PaymentRequest<IRequestSource> CreateAlternativePaymentMethodRequest(IRequestSource alternativePaymentMethodRequestSource, string currency, long? amount = 100)
        {
            return new PaymentRequest<IRequestSource>(
                alternativePaymentMethodRequestSource,
                currency,
                amount
            )
            {
                Capture = false,
                Customer = new Checkout.Payments.CustomerRequest() { Email = TestHelper.GenerateRandomEmail() },
                Reference = Guid.NewGuid().ToString()
            };
        }

        public static CardTokenRequest CreateCardTokenRequest()
        {
            return new CardTokenRequest(TestCardSource.Visa.Number, TestCardSource.Visa.ExpiryMonth,
                TestCardSource.Visa.ExpiryYear)
            {
                Cvv = TestCardSource.Visa.Cvv
            };
        }

        public static PaymentRequest<CardSource> CreateChargebackCardPaymentRequest()
        {
            return new PaymentRequest<CardSource>(
                new CardSource(TestCardSource.Visa.Number, TestCardSource.Visa.ExpiryMonth, TestCardSource.Visa.ExpiryYear)
                {
                    Cvv = TestCardSource.Visa.Cvv
                },
                Currency.GBP,
                1040
            )
            {
                Capture = true,
                Customer = new Checkout.Payments.CustomerRequest() { Email = TestHelper.GenerateRandomEmail() },
                Reference = Guid.NewGuid().ToString()
            };
        }

        public static string GenerateRandomEmail()
        {
            return Guid.NewGuid().ToString("n") + "@checkout-sdk-net.com";
        }

        public static int PaymentDisputedVerificationInterval()
        {
            return 10000;
        }

        public static PaymentRequest<TokenSource> CreateTokenPaymentRequest(string token)
        {
            return new PaymentRequest<TokenSource>(new TokenSource(token),
                    Currency.GBP,
                    100)
                    {
                        Capture = false
                    };
        }
    }
}
