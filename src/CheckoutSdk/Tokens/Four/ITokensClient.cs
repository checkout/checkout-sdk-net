using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Tokens.Four
{
    public interface ITokensClient
    {
        Task<CardTokenResponse> Request(CardTokenRequest cardTokenRequest,
            CancellationToken cancellationToken = default);

        Task<TokenResponse> Request(TokenRequest tokenRequest, CancellationToken cancellationToken = default);
    }
}