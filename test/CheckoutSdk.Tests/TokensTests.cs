using System;
using System.Net;
using Checkout.Tokens;
using Shouldly;

namespace Checkout.Tests
{
    class describe_tokens : ApiTest
    {
        void describe_card_tokens()
        {
            CardTokenRequest request = null;
            ApiResponse<CardTokenResponse> response = null;

            before = () => request = new CardTokenRequest(TestCard.Visa.Number, TestCard.Visa.ExpiryMonth, TestCard.Visa.ExpiryYear)
            {
                Cvv = TestCard.Visa.Cvv,
                BillingAddress = new Address
                {
                    AddressLine1 = "Checkout.com",
                    AddressLine2 = "90 Tottenham Court Road",
                    City = "London",
                    State = "London",
                    Zip = "W1T 4TJ",
                    Country = "GB"
                },
                Phone = new Phone
                {
                    CountryCode = "44",
                    Number = "020 222333"
                }
            };

            actAsync = async () => response = await Api.Tokens.RequestAsync(request);

            it["returns card token"] = () =>
            {
                response.StatusCode.ShouldBe(HttpStatusCode.Created);
                var token = response.Result;

                token.ShouldNotBeNull();
                token.Token.ShouldNotBeNullOrEmpty();
                token.ExpiresOn.ShouldBeGreaterThan(DateTime.UtcNow);
                token.BillingAddress.ShouldNotBeNull();
                token.BillingAddress.AddressLine1.ShouldBe(request.BillingAddress.AddressLine1);
                token.BillingAddress.AddressLine2.ShouldBe(request.BillingAddress.AddressLine2);
                token.BillingAddress.City.ShouldBe(request.BillingAddress.City);
                token.BillingAddress.State.ShouldBe(request.BillingAddress.State);
                token.BillingAddress.Zip.ShouldBe(request.BillingAddress.Zip);
                token.BillingAddress.Country.ShouldBe(request.BillingAddress.Country);
                token.Phone.ShouldNotBeNull();
                token.Phone.CountryCode.ShouldBe(request.Phone.CountryCode);
                token.Phone.Number.ShouldBe(request.Phone.Number);
                token.Type.ShouldBe("card");
                token.ExpiryMonth.ShouldBe(request.ExpiryMonth);
                token.ExpiryYear.ShouldBe(request.ExpiryYear);
            };
        }
    }
}