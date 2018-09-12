using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sdk.Tokens
{
    public interface ITokensClient
    {
        Task<CardTokenResponse> RequestAsync(CardTokenRequest cardTokenRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<TokenResponse> RequestAsync(WalletTokenRequest walletTokenRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}