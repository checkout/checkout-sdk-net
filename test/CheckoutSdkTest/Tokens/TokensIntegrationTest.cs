using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
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
            var cardTokenRequest = new CardTokenRequest
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = GetAddress(),
                Phone = GetPhone()
            };

            var cardTokenResponse = await DefaultApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();

            cardTokenResponse.Token.ShouldNotBeNullOrEmpty();
            cardTokenResponse.Type.ShouldBe(TokenType.Card);
            cardTokenResponse.ExpiryMonth.ShouldBe(cardTokenRequest.ExpiryMonth);
            cardTokenResponse.ExpiryYear.ShouldBe(cardTokenRequest.ExpiryYear);
            cardTokenResponse.ExpiresOn.ShouldNotBeNull();
            cardTokenResponse.CardType.ShouldBe(CardType.Credit);
            cardTokenResponse.CardCategory.ShouldBe(CardCategory.Consumer);
        }
    }
}