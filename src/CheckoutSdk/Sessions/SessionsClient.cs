using Checkout.Common;
using Checkout.Sessions.Channel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sessions
{
    public class SessionsClient : AbstractClient, ISessionsClient
    {
        private const string SessionsPath = "sessions";
        private const string CollectDataPath = "collect-data";
        private const string CompletePath = "complete";
        private const string IssuerFingerprintPath = "issuer-fingerprint";
        private const string SessionIdPath = "sessionId";

        private static readonly IDictionary<int, Type> SessionResponseMappings = new Dictionary<int, Type>();

        static SessionsClient()
        {
            SessionResponseMappings[201] = typeof(CreateSessionOkResponse);
            SessionResponseMappings[202] = typeof(CreateSessionAcceptedResponse);
        }

        public SessionsClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public async Task<SessionResponse> RequestSession(SessionRequest sessionRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionRequest", sessionRequest);
            return await CreateSession(sessionRequest, cancellationToken);
        }

        public async Task<GetSessionResponse> GetSessionDetails(string sessionId, CancellationToken cancellationToken = default)
        {
            return await GetSessionDetails(sessionId, SdkAuthorization(), cancellationToken);
        }

        public async Task<GetSessionResponse> GetSessionDetails(string sessionSecret, string sessionId, CancellationToken cancellationToken = default)
        {
            return await GetSessionDetails(sessionId, SessionSecretAuthorization(sessionSecret), cancellationToken);
        }

        public async Task<GetSessionResponse> UpdateSession(string sessionId, ChannelData channelData, CancellationToken cancellationToken = default)
        {
            return await UpdateSession(sessionId, channelData, SdkAuthorization(), cancellationToken);
        }

        public async Task<GetSessionResponse> UpdateSession(string sessionSecret, string sessionId, ChannelData channelData, CancellationToken cancellationToken = default)
        {
            return await UpdateSession(sessionId, channelData, SessionSecretAuthorization(sessionSecret), cancellationToken);
        }

        public async Task CompleteSession(string sessionId, CancellationToken cancellationToken = default)
        {
            await CompleteSession(sessionId, SdkAuthorization(), cancellationToken);
        }

        public async Task CompleteSession(string sessionSecret, string sessionId, CancellationToken cancellationToken = default)
        {
            await CompleteSession(sessionId, SessionSecretAuthorization(sessionSecret), cancellationToken);
        }

        public Task<GetSessionResponseAfterChannelDataSupplied> Update3dsMethodCompletionIndicator(string sessionId, ThreeDsMethodCompletionRequest threeDsMethodCompletionRequest, CancellationToken cancellationToken = default)
        {
            return Update3dsMethodCompletionIndicator(sessionId, threeDsMethodCompletionRequest, SdkAuthorization(), cancellationToken);
        }

        public Task<GetSessionResponseAfterChannelDataSupplied> Update3dsMethodCompletionIndicator(string sessionSecret, string sessionId, ThreeDsMethodCompletionRequest threeDsMethodCompletionRequest, CancellationToken cancellationToken = default)
        {
            return Update3dsMethodCompletionIndicator(sessionId, threeDsMethodCompletionRequest, SessionSecretAuthorization(sessionSecret), cancellationToken);
        }

        private async Task<SessionResponse> CreateSession(SessionRequest sessionRequest, CancellationToken cancellationToken = default)
        {
            var resource = await ApiClient.Post<Resource>(SessionsPath, SdkAuthorization(), SessionResponseMappings, sessionRequest, cancellationToken);

            switch (resource)
            {
                case CreateSessionOkResponse sessionOK:
                    return new SessionResponse(sessionOK);

                case CreateSessionAcceptedResponse sessionAccepted:
                    return new SessionResponse(sessionAccepted);

                default:
                    throw new System.InvalidOperationException("Unexpected mapping type " + resource.GetType());
            }
        }

        private async Task<GetSessionResponse> GetSessionDetails(string sessionId, SdkAuthorization sdkAuthorization, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams(SessionIdPath, sessionId, "sdkAuthorization", sdkAuthorization);
            return await ApiClient.Get<GetSessionResponse>(BuildPath(SessionsPath, sessionId), sdkAuthorization, cancellationToken);
        }

        private async Task<GetSessionResponse> UpdateSession(string sessionId, ChannelData channelData, SdkAuthorization sdkAuthorization,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams(SessionIdPath, sessionId, "channelData", channelData, "sdkAuthorization", sdkAuthorization);
            return await ApiClient.Put<GetSessionResponse>(BuildPath(SessionsPath, sessionId, CollectDataPath), sdkAuthorization, channelData, cancellationToken);
        }

        private async Task CompleteSession(string sessionId, SdkAuthorization sdkAuthorization, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams(SessionIdPath, sessionId, "sdkAuthorization", sdkAuthorization);
            await ApiClient.Post<object>(BuildPath(SessionsPath, sessionId, CompletePath), sdkAuthorization, null, cancellationToken);
        }

        private async Task<GetSessionResponseAfterChannelDataSupplied> Update3dsMethodCompletionIndicator(string sessionId, ThreeDsMethodCompletionRequest threeDsMethodCompletionRequest,
            SdkAuthorization sdkAuthorization, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams(SessionIdPath, sessionId, "threeDsMethodCompletionRequest", threeDsMethodCompletionRequest, "sdkAuthorization", sdkAuthorization);
            return await ApiClient.Put<GetSessionResponseAfterChannelDataSupplied>(BuildPath(SessionsPath, sessionId, IssuerFingerprintPath), sdkAuthorization, threeDsMethodCompletionRequest, cancellationToken);
        }

        private SdkAuthorization SessionSecretAuthorization(string sessionSecret)
        {
            CheckoutUtils.ValidateParams("sessionSecret", sessionSecret);
            return (new SessionSecretSdkCredentials(sessionSecret)).GetSdkAuthorization(SdkAuthorizationType.Custom);
        }
    }
}