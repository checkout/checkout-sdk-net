using Checkout.Common;
using Checkout.Metadata.Card.Types;
using Checkout.Metadata.Sources;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Metadata.Card
{
    public class MetadataCardIntegrationTest : SandboxTestFixture
    {
        private static readonly string BinNumberConstant = TestCardSource.Visa.Number[..8];
        private static readonly string CardNumberConstant = TestCardSource.Visa.Number;
        private const string PaymentSourceRequiredErrorConstant = "payment_source_required";

        public MetadataCardIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        private async Task ShouldRequestCardMetadataForCardNumber()
        {
            CardMetadataRequest request = new CardMetadataRequest
            {
                Source = new CardSource { Number = CardNumberConstant }, Format = CardMetadataFormatType.Basic
            };
            await MetadataClientCardResponseBasicAssertion(request);
        }

        [Fact]
        private async Task ShouldRequestCardMetadataForBinNumber()
        {
            CardMetadataRequest request = new CardMetadataRequest
            {
                Source = new BinSource { Bin = BinNumberConstant }, Format = CardMetadataFormatType.Basic
            };
            await MetadataClientCardResponseBasicAssertion(request);
        }
        
        private async Task MetadataClientCardResponseBasicAssertion(CardMetadataRequest request)
        {
            CardMetadataResponse response = await DefaultApi.MetadataClient().RequestCardMetadata(request);

            response.ShouldNotBeNull();
            response.Bin.ShouldBe(BinNumberConstant);
            response.Scheme.ShouldNotBeNull();
            response.CardType.ShouldBeOfType<CardType>();
            response.CardCategory.ShouldBeOfType<CardCategory>();
            response.IssuerCountry.ShouldBeOfType<CountryCode>();
            response.IssuerCountryName.ShouldNotBeNull();
            response.ProductId.ShouldNotBeNull();
            response.ProductType.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(200);
        }
        
        [Fact]
        private async Task ShouldRequestCardMetadataForEmptyRequestError()
        {
            CardMetadataRequest request = new CardMetadataRequest
            {
                Source = new BinSource { Bin = BinNumberConstant }, Format = CardMetadataFormatType.Basic
            };

            await CheckErrorItem(async () => await DefaultApi.MetadataClient().RequestCardMetadata(request),
                PaymentSourceRequiredErrorConstant);
        }
    }
}