using Checkout.Issuing.CardholderAccessTokens.Requests;
using Checkout.Issuing.CardholderAccessTokens.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        /// <summary>
        /// Request an access token
        /// OAuth endpoint to exchange your access key ID and access key secret for an access token
        /// </summary>
        Task<CardholderAccessTokenResponse> RequestCardholderAccessToken(
            CardholderAccessTokenRequest cardholderAccessTokenRequest,
            CancellationToken cancellationToken = default);
    }
}