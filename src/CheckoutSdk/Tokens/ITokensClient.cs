using System.Threading.Tasks;

namespace Checkout.Tokens
{
    public interface ITokensClient
    {
        Task<ApiResponse<CardTokenResponse>> RequestAsync(CardTokenRequest cardTokenRequest);
        Task<ApiResponse<WalletTokenRequest>> RequestAsync(WalletTokenRequest WalletTokenRequest);
    }
}