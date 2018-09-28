using Shouldly;
using System;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Tokens;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests
{
    public class TokensTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public TokensTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanTokenizeCard()
        {
            CardTokenRequest request = CreateValidRequest();

            CardTokenResponse token = await _api.Tokens.RequestAsync(request);

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
        }

        private CardTokenRequest CreateValidRequest()
        {
            return new CardTokenRequest(TestCardSource.Visa.Number, TestCardSource.Visa.ExpiryMonth, TestCardSource.Visa.ExpiryYear)
            {
                Cvv = TestCardSource.Visa.Cvv,
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
        }
    }
}