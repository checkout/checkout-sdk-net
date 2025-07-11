using Checkout.NetworkTokens.PatchDelete.Requests;
using Checkout.NetworkTokens.PostCryptograms.Requests;
using Checkout.NetworkTokens.PostNetworkTokens.Requests;
using Checkout.NetworkTokens.PostNetworkTokens.Requests.Sources;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.NetworkTokens
{
    public class NetworkTokenIntegrationTest : SandboxTestFixture
    {
        public NetworkTokenIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "use on demand")]
        private async Task ShouldProvisionANetworkToken()
        {
            var request = new ProvisionANetworkTokenRequest
            {
                Source = new IdSource()
                {
                    Id = "src_wmlfc3zyhqzehihu7giusaaawu"
                },
            };

            var response = await DefaultApi.NetworkTokensClient().ProvisionANetworkToken(request);

            response.ShouldNotBeNull();
            response.Card.ShouldNotBeNull();
            response.NetworkToken.ShouldNotBeNull();
        }
        
        [Fact(Skip = "use on demand")]
        private async Task ShouldGetNetworkToken()
        {
            var networkTokenId = "nt_xgu3isllqfyu7ktpk5z2yxbwna";
            var response = await DefaultApi.NetworkTokensClient().GetNetworkToken(networkTokenId);

            response.ShouldNotBeNull();
            response.Card.ShouldNotBeNull();
            response.NetworkToken.ShouldNotBeNull();
        }

        [Fact(Skip = "use on demand")]
        private async Task ShouldGetNetworkTokens()
        {
            var networkTokenId = "nt_xgu3isllqfyu7ktpk5z2yxbwna";

            var request = new NetworkTokenCryptogramRequest() { TransactionType = TransactionType.Ecom };

            var response = await DefaultApi.NetworkTokensClient().RequestACryptogram(networkTokenId, request);

            response.ShouldNotBeNull();
            response.Cryptogram.ShouldNotBeEmpty();
        }

        [Fact(Skip = "use on demand")]
        private async Task ShouldPatchNetworkToken()
        {
            var networkTokenId = "nt_xgu3isllqfyu7ktpk5z2yxbwna";

            var request = new PermanentlyDeleteANetworkTokenRequest
            {
                InitiatedBy = InitiatedByType.TokenRequestor, Reason = ReasonType.Other
            };

            var response = await DefaultApi.NetworkTokensClient()
                .PermanentlyDeletesANetworkToken(networkTokenId, request);

            response.HttpStatusCode.ShouldBe(204);
        }
    }
}
