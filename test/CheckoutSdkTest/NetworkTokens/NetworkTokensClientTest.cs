using Checkout.NetworkTokens.GetNetworkTokens.Responses;
using Checkout.NetworkTokens.PatchDelete.Requests;
using Checkout.NetworkTokens.PostCryptograms.Requests;
using Checkout.NetworkTokens.PostCryptograms.Responses;
using Checkout.NetworkTokens.PostNetworkTokens.Requests;
using Checkout.NetworkTokens.PostNetworkTokens.Responses;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.NetworkTokens
{
    public class NetworkTokensClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;
        
        public NetworkTokensClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }
        
        [Fact]
        private async Task ShouldProvisionANetworkToken()
        {
            ProvisionANetworkTokenRequest provisionANetworkTokenRequest = new ProvisionANetworkTokenRequest();
            ProvisionANetworkTokenResponse provisionANetworkTokenResponse = new ProvisionANetworkTokenResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ProvisionANetworkTokenResponse>(
                        "network-tokens",
                        _authorization,
                        provisionANetworkTokenRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => provisionANetworkTokenResponse);

            INetworkTokensClient client =
                new NetworkTokensClient(_apiClient.Object, _configuration.Object);

            ProvisionANetworkTokenResponse response = await client.ProvisionANetworkToken(provisionANetworkTokenRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(provisionANetworkTokenResponse);
        }
        
        [Fact]
        private async Task ShouldGetNetworkToken()
        {
            string networkTokenId = "network_token_id";
            NetworkTokenByIdResponse networkTokenByIdResponse = new NetworkTokenByIdResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<NetworkTokenByIdResponse>(
                        "network-tokens/" + networkTokenId,
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => networkTokenByIdResponse);

            INetworkTokensClient client =
                new NetworkTokensClient(_apiClient.Object, _configuration.Object);

            NetworkTokenByIdResponse response = await client.GetNetworkToken(networkTokenId);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(networkTokenByIdResponse);
        }
        
        [Fact]
        private async Task ShouldRequestACryptogram()
        {
            string networkTokenId = "network_token_id";
            NetworkTokenCryptogramRequest networkTokenCryptogramRequest = new NetworkTokenCryptogramRequest();
            NetworkTokenCryptogramResponse networkTokenCryptogramResponse = new NetworkTokenCryptogramResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<NetworkTokenCryptogramResponse>(
                        "network-tokens/" + networkTokenId + "/cryptograms",
                        _authorization,
                        networkTokenCryptogramRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => networkTokenCryptogramResponse);

            INetworkTokensClient client =
                new NetworkTokensClient(_apiClient.Object, _configuration.Object);

            NetworkTokenCryptogramResponse response = await client.RequestACryptogram(networkTokenId, networkTokenCryptogramRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(networkTokenCryptogramResponse);
        }
        
        [Fact]
        private async Task ShouldPermanentlyDeleteANetworkToken()
        {
            string networkTokenId = "network_token_id";
            PermanentlyDeleteANetworkTokenRequest permanentlyDeleteANetworkTokenRequest = new PermanentlyDeleteANetworkTokenRequest();
            EmptyResponse emptyResponse = new EmptyResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<EmptyResponse>(
                        "network-tokens/" + networkTokenId + "/delete",
                        _authorization,
                        permanentlyDeleteANetworkTokenRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => emptyResponse);

            INetworkTokensClient client =
                new NetworkTokensClient(_apiClient.Object, _configuration.Object);

            EmptyResponse response = await client.PermanentlyDeletesANetworkToken(networkTokenId, permanentlyDeleteANetworkTokenRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(emptyResponse);
        }
    }
}