using Checkout.Common;
using Checkout.Metadata.Card;
using Checkout.Metadata.Card.Source;
using Checkout.Tokens;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Metadata
{
    public class MetadataIntegrationTest : SandboxTestFixture
    {
        private static readonly string BinNumberConstant = TestCardSource.Visa.Number[..8];
        private static readonly string CardNumberConstant = TestCardSource.Visa.Number;

        public MetadataIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        private async Task ShouldRequestCardMetadataForCardNumber()
        {
            CardMetadataRequest request = new CardMetadataRequest
            {
                Source = new CardMetadataCardSource { Number = CardNumberConstant },
                Format = CardMetadataFormatType.Basic
            };

            await MakeCardMetadataRequest(request);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldRequestCardMetadataForBinNumber()
        {
            CardMetadataRequest request = new CardMetadataRequest
            {
                Source = new CardMetadataBinSource { Bin = BinNumberConstant },
                Format = CardMetadataFormatType.Basic
            };
            await MakeCardMetadataRequest(request);
        }

        [Fact]
        private async Task ShouldRequestCardMetadataForEmptyRequestError()
        {
            CardMetadataRequest request = new CardMetadataRequest
            {
                Source = new CardMetadataTokenSource { Token = await RequestCardToken() },
                Format = CardMetadataFormatType.Basic
            };

            await MakeCardMetadataRequest(request);
        }

        private async Task MakeCardMetadataRequest(CardMetadataRequest request)
        {
            CardMetadataResponse response = await DefaultApi.MetadataClient().RequestCardMetadata(request);

            response.ShouldNotBeNull();
            response.Bin.ShouldBe(BinNumberConstant);
            response.Scheme.ShouldNotBeNull();
            response.CardType.ShouldBeOfType<CardMetadataType>();
            response.CardCategory.ShouldBeOfType<CardCategory>();
            response.IssuerCountry.ShouldBeOfType<CountryCode>();
            response.IssuerCountryName.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(200);
        }

        private static async Task<string> RequestCardToken()
        {
            var api = CheckoutSdk.Builder().StaticKeys()
                .PublicKey(System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_PUBLIC_KEY"))
                .SecretKey(System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_SECRET_KEY"))
                .Environment(Environment.Sandbox)
                .Build();

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

            var cardTokenResponse = await api.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();
            cardTokenResponse.Token.ShouldNotBeNullOrEmpty();
            return cardTokenResponse.Token;
        }
    }
}