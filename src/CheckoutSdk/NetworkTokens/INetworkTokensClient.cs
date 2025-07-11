using Checkout.NetworkTokens.GetNetworkTokens.Responses;
using Checkout.NetworkTokens.PatchDelete.Requests;
using Checkout.NetworkTokens.PostCryptograms.Requests;
using Checkout.NetworkTokens.PostCryptograms.Responses;
using Checkout.NetworkTokens.PostNetworkTokens.Requests;
using Checkout.NetworkTokens.PostNetworkTokens.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.NetworkTokens
{
    /// <summary>
    /// Network Tokens
    /// </summary>
    public interface INetworkTokensClient
    {
        /// <summary>
        /// Provision a Network Token
        /// [BETA]
        /// Provisions a network token synchronously. If the merchant stores their cards with Checkout.com, then
        /// source ID can be used to request a network token for the given card. If the merchant does not store their
        /// cards with Checkout.com, then card details have to be provided.
        /// </summary>
        Task<ProvisionANetworkTokenResponse> ProvisionANetworkToken(
            ProvisionANetworkTokenRequest provisionANetworkTokenRequest,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Get Network Token
        /// [BETA]
        /// Given network token ID, this endpoint returns network token details: DPAN, expiry date, state, TRID and also
        /// card details like last four and expiry date.
        /// </summary>
        Task<NetworkTokenByIdResponse> GetNetworkToken(
            string networkTokenId,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Request a cryptogram
        /// [BETA]
        /// Using network token ID as an input, this endpoint returns token cryptogram.
        /// </summary>
        Task<NetworkTokenCryptogramResponse> RequestACryptogram(
            string networkTokenId,
            NetworkTokenCryptogramRequest networkTokenCryptogramRequest,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Permanently deletes a network token
        /// [BETA]
        /// This endpoint is for permanently deleting a network token. A network token should be deleted when a payment
        /// instrument it is associated with is removed from file or if the security of the token has been compromised.
        /// </summary>
        Task<EmptyResponse> PermanentlyDeletesANetworkToken(
            string networkTokenId,
            PermanentlyDeleteANetworkTokenRequest permanentlyDeleteANetworkTokenRequest,
            CancellationToken cancellationToken = default);
    }
}
