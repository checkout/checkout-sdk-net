using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Tokens
{
    public interface ITokensClient
    {
        Task<ApiResponse<CardTokenResponse>> RequestAsync(CardTokenRequest cardTokenRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiResponse<TokenResponse>> RequestAsync(WalletTokenRequest walletTokenRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}