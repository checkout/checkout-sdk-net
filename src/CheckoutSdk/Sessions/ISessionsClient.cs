using Checkout.Sessions.Channel;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sessions
{
    public interface ISessionsClient
    {
        Task<SessionResponse> RequestSession(SessionRequest sessionRequest, CancellationToken cancellationToken = default);

        Task<GetSessionResponse> GetSessionDetails(string sessionId, CancellationToken cancellationToken = default);

        Task<GetSessionResponse> GetSessionDetails(string sessionSecret, string sessionId, CancellationToken cancellationToken = default);

        Task<GetSessionResponse> UpdateSession(string sessionId, ChannelData channelData, CancellationToken cancellationToken = default);

        Task<GetSessionResponse> UpdateSession(string sessionSecret, string sessionId, ChannelData channelData, CancellationToken cancellationToken = default);

        Task CompleteSession(string sessionId, CancellationToken cancellationToken = default);

        Task CompleteSession(string sessionSecret, string sessionId, CancellationToken cancellationToken = default);

        Task<GetSessionResponseAfterChannelDataSupplied> Update3dsMethodCompletionIndicator(string sessionId, ThreeDsMethodCompletionRequest threeDsMethodCompletionRequest, CancellationToken cancellationToken = default);

        Task<GetSessionResponseAfterChannelDataSupplied> Update3dsMethodCompletionIndicator(string sessionSecret, string sessionId, ThreeDsMethodCompletionRequest threeDsMethodCompletionRequest, CancellationToken cancellationToken = default);
    }
}