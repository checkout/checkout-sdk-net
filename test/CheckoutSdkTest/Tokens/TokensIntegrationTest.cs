using System.Threading.Tasks;
using Checkout.Common;
using Shouldly;
using Xunit;

namespace Checkout.Tokens
{
    public class TokensIntegrationTest : SandboxTestFixture
    {
        public TokensIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldRequestCardToken()
        {
            var phone = new Phone
            {
                CountryCode = "44",
                Number = "020 222333"
            };

            var billingAddress = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var cardTokenRequest = new CardTokenRequest
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = billingAddress,
                Phone = phone
            };

            var cardTokenResponse = await DefaultApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();

            cardTokenResponse.Token.ShouldNotBeNullOrEmpty();
            cardTokenResponse.Type.ShouldBe(TokenType.Card);
            cardTokenResponse.ExpiryMonth.ShouldBe(cardTokenRequest.ExpiryMonth);
            cardTokenResponse.ExpiryYear.ShouldBe(cardTokenRequest.ExpiryYear);
            cardTokenResponse.ExpiresOn.ShouldNotBeNull();
            cardTokenResponse.BillingAddress.ShouldNotBeNull();
            cardTokenResponse.BillingAddress.AddressLine1.ShouldBe(cardTokenRequest.BillingAddress.AddressLine1);
            cardTokenResponse.BillingAddress.AddressLine2.ShouldBe(cardTokenResponse.BillingAddress.AddressLine2);
            cardTokenResponse.BillingAddress.City.ShouldBe(cardTokenRequest.BillingAddress.City);
            cardTokenResponse.BillingAddress.State.ShouldBe(cardTokenRequest.BillingAddress.State);
            cardTokenResponse.BillingAddress.Zip.ShouldBe(cardTokenRequest.BillingAddress.Zip);
            cardTokenResponse.BillingAddress.Country.ShouldBe(cardTokenRequest.BillingAddress.Country);
            cardTokenResponse.Phone.ShouldNotBeNull();
            cardTokenResponse.Phone.CountryCode.ShouldBe(cardTokenRequest.Phone.CountryCode);
            cardTokenResponse.Phone.Number.ShouldBe(cardTokenRequest.Phone.Number);
        }
    }
}