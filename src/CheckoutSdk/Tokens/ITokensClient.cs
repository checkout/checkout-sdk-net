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
    }
}