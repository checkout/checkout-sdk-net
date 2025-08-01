using Checkout.Authentication.Standalone.GETSessionsId.Responses.GetSessionDetailsResponseOk;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest;
using Checkout.Authentication.Standalone.POSTSessions.Responses;
using Checkout.Authentication.Standalone.POSTSessionsIdComplete.Responses.CompleteASessionResponseNoContent;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Responses.UpdateASessionResponseOk;
using Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Requests.
    UpdateSessionThreeDSMethodCompletionIndicatorRequest;
using Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Responses.
    UpdateSessionThreedsMethodCompletionIndicatorResponseOk;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Authentication
{
    public interface IAuthenticationClient
    {
        Task<RequestASessionResponse> RequestASession(RequestASessionRequest requestASessionRequest,
            CancellationToken cancellationToken = default);

        Task<GetSessionDetailsResponseOk> GetSessionDetails(string sessionId,
            CancellationToken cancellationToken = default);

        Task<GetSessionDetailsResponseOk> GetSessionDetails(string sessionSecret, string sessionId,
            CancellationToken cancellationToken = default);

        Task<UpdateASessionResponseOk> UpdateASession(string sessionId,
            AbstractUpdateASessionRequest updateASessionRequest,
            CancellationToken cancellationToken = default);

        Task<UpdateASessionResponseOk> UpdateASession(string sessionSecret, string sessionId,
            AbstractUpdateASessionRequest updateASessionRequest,
            CancellationToken cancellationToken = default);

        Task<CompleteASessionResponseNoContent> CompleteASession(string sessionId,
            CancellationToken cancellationToken = default);

        Task<CompleteASessionResponseNoContent> CompleteASession(string sessionSecret, string sessionId,
            CancellationToken cancellationToken = default);

        Task<UpdateSessionThreedsMethodCompletionIndicatorResponseOk> UpdateSessionThreedsMethodCompletionIndicator(
            string sessionId,
            UpdateSessionThreedsMethodCompletionIndicatorRequest updateSessionThreedsMethodCompletionIndicatorRequest,
            CancellationToken cancellationToken = default);

        Task<UpdateSessionThreedsMethodCompletionIndicatorResponseOk> UpdateSessionThreedsMethodCompletionIndicator(
            string sessionSecret,
            string sessionId,
            UpdateSessionThreedsMethodCompletionIndicatorRequest updateSessionThreedsMethodCompletionIndicatorRequest,
            CancellationToken cancellationToken = default);
    }
}