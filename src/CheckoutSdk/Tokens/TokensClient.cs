using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Tokens
{
    public class TokensClient : AbstractClient, ITokensClient
    {
        private const string Tokens = "tokens";

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
            return ApiClient.Post<CardTokenResponse>(Tokens, SdkAuthorization(), cardTokenRequest, cancellationToken);
        }

        public Task<TokenResponse> Request(
            WalletTokenRequest walletTokenRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("walletTokenRequest", walletTokenRequest);
            return ApiClient.Post<TokenResponse>(Tokens, SdkAuthorization(), walletTokenRequest, cancellationToken);
        }
    }
}