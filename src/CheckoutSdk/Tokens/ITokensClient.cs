using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Tokens
{
    public interface ITokensClient
    {
        Task<CardTokenResponse> Request(CardTokenRequest cardTokenRequest,
            CancellationToken cancellationToken = default);

        Task<TokenResponse> Request(WalletTokenRequest walletTokenRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the details for an active token without consuming it. The token remains usable
        /// after this call.
        /// </summary>
        /// <param name="tokenId">The token ID. Pattern: ^(tok)_(\w{26})$.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<TokenMetadataResponse> GetTokenMetadata(string tokenId,
            CancellationToken cancellationToken = default);
    }
}