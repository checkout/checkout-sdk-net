using System;
using Checkout.Common;
using Checkout.Payments;
using Checkout.Sources;
using Checkout.Tokens;

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
        public static PaymentRequest<IRequestSource> CreateAlternativePaymentMethodRequest(IRequestSource alternativePaymentMethodRequestSource, string currency, int? amount = 100)
        {
            return new PaymentRequest<IRequestSource>(
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

        public static CardTokenRequest CreateCardTokenRequest()
        {
            return new CardTokenRequest(TestCardSource.Visa.Number, TestCardSource.Visa.ExpiryMonth,
                TestCardSource.Visa.ExpiryYear)
            {
                Cvv = TestCardSource.Visa.Cvv
            };
        }

        public static string GenerateRandomEmail()
        {
            return Guid.NewGuid().ToString("n") + "@checkout-sdk-net.com";
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

        public static SourceRequest CreateSourceRequest()
        {
            return new SourceRequest(
                type: "sepa",
                billingAddress: new Address()
                {
                    AddressLine1 = "Checkout.com",
                    AddressLine2 = "90 Tottenham Court Road",
                    City = "London",
                    State = "London",
                    Zip = "W1T 4TJ",
                    Country = "GB"
                })
            {
                Reference = ".NET SDK test",
                Phone = new Phone()
                {
                    CountryCode = "+1",
                    Number = "415 555 2671"
                },
                SourceData = new SourceData()
                {
                    FirstName = "Marcus",
                    LastName = "Barrilius Maximus",
                    Iban = "DE68100100101234567895",
                    Bic = "PBNKDEFFXXX",
                    BillingDescriptor = ".NET SDK test",
                    MandateType = "single"
                }
            };
        }
    }
}
