using Checkout.Forward.Requests;
using Checkout.Forward.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Forward
{
    public interface IForwardClient
    {
        /// <summary>
        ///     Forward an API request<br />
        ///     <b>Beta</b><br />
        ///     Forwards an API request to a third-party endpoint.<br />
        ///     For example, you can forward payment credentials you've stored in our Vault to a third-party payment processor.
        /// </summary>
        Task<ForwardAnApiResponse> ForwardAnApiRequest(ForwardRequest forwardRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get forward request<br />
        ///     Retrieve the details of a successfully forwarded API request.<br />
        ///     The details can be retrieved for up to 14 days after the request was initiated.
        /// </summary>
        Task<GetForwardResponse> GetForwardRequest(string forwardId, CancellationToken cancellationToken = default);
    }
}