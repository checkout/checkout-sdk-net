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
            var cardTokenRequest = CreateCardTokenRequest();

            var cardTokenResponse = await DefaultApi.TokensClient().Request(cardTokenRequest);

            ValidateCardTokenResponse(cardTokenResponse, cardTokenRequest);
        }

        [Fact(Skip = "Requires secret-key scope tokens:metadata which is not provisioned by default")]
        private async Task ShouldGetTokenMetadata()
        {
            var cardTokenRequest = CreateCardTokenRequest();
            var cardTokenResponse = await DefaultApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.Token.ShouldNotBeNullOrEmpty();

            var metadata = await DefaultApi.TokensClient().GetTokenMetadata(cardTokenResponse.Token);

            ValidateTokenMetadataResponse(metadata, cardTokenResponse.Token, cardTokenRequest);
        }

        private static CardTokenRequest CreateCardTokenRequest()
        {
            return new CardTokenRequest
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = GetAddress(),
                Phone = GetPhone()
            };
        }

        private static void ValidateCardTokenResponse(CardTokenResponse response, CardTokenRequest request)
        {
            response.ShouldNotBeNull();
            response.Token.ShouldNotBeNullOrEmpty();
            response.Type.ShouldBe(TokenType.Card);
            response.ExpiryMonth.ShouldBe(request.ExpiryMonth);
            response.ExpiryYear.ShouldBe(request.ExpiryYear);
            response.ExpiresOn.ShouldNotBeNull();
            response.CardType.ShouldBe(CardType.Credit);
            response.CardCategory.ShouldBe(CardCategory.Consumer);
        }

        private static void ValidateTokenMetadataResponse(TokenMetadataResponse response, string token, CardTokenRequest request)
        {
            response.ShouldNotBeNull();
            response.Token.ShouldBe(token);
            response.Type.ShouldBe("card");
            response.ExpiryMonth.ShouldBe(request.ExpiryMonth);
            response.ExpiryYear.ShouldBe(request.ExpiryYear);
            response.Last4.ShouldNotBeNullOrEmpty();
            response.Bin.ShouldNotBeNullOrEmpty();
            response.ExpiresOn.ShouldNotBeNull();
        }
    }
}
