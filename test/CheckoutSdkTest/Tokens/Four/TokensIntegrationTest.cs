using System.Threading.Tasks;
using Checkout.Common;
using Shouldly;
using Xunit;

namespace Checkout.Tokens.Four
{
    public class TokensIntegrationTest : SandboxTestFixture
    {
        public TokensIntegrationTest() : base(PlatformType.Four)
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

            var cardTokenResponse = await FourApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();

            cardTokenResponse.Token.ShouldNotBeNullOrEmpty();
            cardTokenResponse.Type.ShouldBe(TokenType.Card);
            cardTokenResponse.ExpiryMonth.ShouldBe(cardTokenRequest.ExpiryMonth);
            cardTokenResponse.ExpiryYear.ShouldBe(cardTokenRequest.ExpiryYear);
            cardTokenResponse.ExpiresOn.ShouldNotBeNull();
        }
    }
}