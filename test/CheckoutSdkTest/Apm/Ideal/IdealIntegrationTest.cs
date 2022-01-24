using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Checkout.Apm.Ideal
{
    public class IdealIntegrationTest : SandboxTestFixture
    {
        public IdealIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "unstable")]
        private async Task ShouldGetInfo()
        {
            var idealInfo = await DefaultApi.IdealClient().GetInfo();

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
            var idealInfo = await DefaultApi.IdealClient().GetIssuers();

            idealInfo.ShouldNotBeNull();
            idealInfo.GetSelfLink().ShouldNotBeNull();
            idealInfo.Countries.ShouldNotBeNull();
            idealInfo.Countries.Count.ShouldBePositive();
        }
    }
}