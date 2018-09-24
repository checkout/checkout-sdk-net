using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Tokens
{
    public interface ITokensClient
    {
        /// <summary>
        /// Exchange card details for a reference token that can be later used to request a card payment.
        /// To create tokens please authenticate using your public key
        /// </summary>
        /// <param name="cardTokenRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CardTokenResponse> RequestAsync(CardTokenRequest cardTokenRequest, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Exchange digital wallet payment token for a reference token that can be later used to request a card payment.
        /// To create tokens please authenticate using your public key
        /// </summary>
        /// <param name="cardTokenRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TokenResponse> RequestAsync(WalletTokenRequest walletTokenRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}