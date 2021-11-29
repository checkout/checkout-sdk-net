using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Tokens.Four
{
    public class TokensClient : AbstractClient, ITokensClient
    {
        public TokensClient(
            IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.PublicKey)
        {
        }

        public Task<CardTokenResponse> Request(
            CardTokenRequest cardTokenRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardTokenRequest", cardTokenRequest);
            return ApiClient.Post<CardTokenResponse>("tokens", SdkAuthorization(), cardTokenRequest, cancellationToken);
        }

        public Task<TokenResponse> Request(
            TokenRequest tokenRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("tokenRequest", tokenRequest);
            return ApiClient.Post<TokenResponse>("tokens", SdkAuthorization(), tokenRequest, cancellationToken);
        }
    }
}