using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.DigitalCards
{
    public class DigitalCardsIntegrationTest : SandboxTestFixture
    {
        public DigitalCardsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "Requires a valid digital card ID from a provisioned card")]
        public async Task GetDigitalCard_ShouldReturnDigitalCardDetails()
        {
            const string digitalCardId = "dcr_5ngxzsynm2me3oxf73esbhda6q";

            var response = await DefaultApi.IssuingClient().GetDigitalCard(digitalCardId);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(digitalCardId);
            response.CardId.ShouldNotBeNullOrEmpty();
            response.LastFour.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNull();
        }
    }
}
