using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Apm.Ideal
{
    public class IdealIntegrationTest : SandboxTestFixture
    {
        public IdealIntegrationTest() : base(PlatformType.Previous)
        {
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldGetInfo()
        {
            var idealInfo = await PreviousApi.IdealClient().GetInfo();

            idealInfo.ShouldNotBeNull();
            idealInfo.Links.ShouldNotBeNull();
            idealInfo.Links.Self.ShouldNotBeNull();
            idealInfo.Links.Issuers.ShouldNotBeNull();
            idealInfo.Links.Curies.ShouldNotBeNull();
            idealInfo.Links.Curies.Count.ShouldBePositive();
        }

        [Fact]
        private async Task ShouldGetIssuers()
        {
            var idealInfo = await PreviousApi.IdealClient().GetIssuers();

            idealInfo.ShouldNotBeNull();
            idealInfo.GetSelfLink().ShouldNotBeNull();
            idealInfo.Countries.ShouldNotBeNull();
            idealInfo.Countries.Count.ShouldBePositive();
        }
    }
}