using Checkout.Authentication.Standalone.GETSessionsId.Responses.GetSessionDetailsResponseOk;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest;
using Checkout.Authentication.Standalone.POSTSessions.Responses;
using Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseAccepted;
using Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseCreated;
using Checkout.Authentication.Standalone.POSTSessionsIdComplete.Responses.CompleteASessionResponseNoContent;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Responses.UpdateASessionResponseOk;
using Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Requests.
    UpdateSessionThreeDSMethodCompletionIndicatorRequest;
using Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Responses.
    UpdateSessionThreedsMethodCompletionIndicatorResponseOk;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Authentication
{
    public class AuthenticationClient : AbstractClient, IAuthenticationClient
    {
        private const string SessionsPath = "sessions";
        private const string CollectDataPath = "collect-data";
        private const string CompletePath = "complete";
        private const string IssuerFingerprintPath = "issuer-fingerprint";
        private const string SessionIdPath = "sessionId";

        private static readonly IDictionary<int, Type> RequestASessionResponseMappings = new Dictionary<int, Type>();

        static AuthenticationClient()
        {
            RequestASessionResponseMappings[201] = typeof(RequestASessionResponseCreated);
            RequestASessionResponseMappings[202] = typeof(RequestASessionResponseAccepted);
        }

        public AuthenticationClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public async Task<RequestASessionResponse> RequestASession(RequestASessionRequest requestASessionRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("requestASessionRequest", requestASessionRequest);
            var resource = await ApiClient.Post<HttpMetadata>(SessionsPath, SdkAuthorization(),
                RequestASessionResponseMappings,
                requestASessionRequest, cancellationToken);

            switch (resource)
            {
                case RequestASessionResponseCreated requestASessionResponseCreated:
                    return new RequestASessionResponse(requestASessionResponseCreated);

                case RequestASessionResponseAccepted requestASessionResponseAccepted:
                    return new RequestASessionResponse(requestASessionResponseAccepted);

                default:
                    throw new InvalidOperationException("Unexpected mapping type " + resource.GetType());
            }
        }

        public async Task<GetSessionDetailsResponseOk> GetSessionDetails(string sessionId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionId", sessionId);
            return await GetSessionDetails(sessionId, SdkAuthorization(), cancellationToken);
        }

        public async Task<GetSessionDetailsResponseOk> GetSessionDetails(string sessionSecret, string sessionId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionSecret", sessionSecret, "sessionId", sessionId);
            return await GetSessionDetails(sessionId, SessionSecretAuthorization(sessionSecret), cancellationToken);
        }

        public async Task<UpdateASessionResponseOk> UpdateASession(string sessionId,
            AbstractUpdateASessionRequest updateASessionRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionId", sessionId, "updateASessionRequest", updateASessionRequest);
            return await UpdateASession(sessionId, updateASessionRequest, SdkAuthorization(), cancellationToken);
        }

        public async Task<UpdateASessionResponseOk> UpdateASession(string sessionSecret, string sessionId,
            AbstractUpdateASessionRequest updateASessionRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionSecret", sessionSecret, "sessionId", sessionId,
                "updateASessionRequest",
                updateASessionRequest);
            return await UpdateASession(sessionId, updateASessionRequest, SessionSecretAuthorization(sessionSecret),
                cancellationToken);
        }

        public async Task<CompleteASessionResponseNoContent> CompleteASession(string sessionId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionId", sessionId);
            return await CompleteASession(sessionId, SdkAuthorization(), cancellationToken);
        }

        public async Task<CompleteASessionResponseNoContent> CompleteASession(string sessionSecret, string sessionId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionSecret", sessionSecret, "sessionId", sessionId);
            return await CompleteASession(sessionId, SessionSecretAuthorization(sessionSecret), cancellationToken);
        }

        public Task<UpdateSessionThreedsMethodCompletionIndicatorResponseOk>
            UpdateSessionThreedsMethodCompletionIndicator(string sessionId,
                UpdateSessionThreedsMethodCompletionIndicatorRequest
                    updateSessionThreedsMethodCompletionIndicatorRequest,
                CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionId", sessionId, "updateSessionThreedsMethodCompletionIndicatorRequest",
                updateSessionThreedsMethodCompletionIndicatorRequest);
            return UpdateSessionThreedsMethodCompletionIndicator(sessionId,
                updateSessionThreedsMethodCompletionIndicatorRequest, SdkAuthorization(),
                cancellationToken);
        }

        public Task<UpdateSessionThreedsMethodCompletionIndicatorResponseOk>
            UpdateSessionThreedsMethodCompletionIndicator(string sessionSecret,
                string sessionId,
                UpdateSessionThreedsMethodCompletionIndicatorRequest
                    updateSessionThreedsMethodCompletionIndicatorRequest,
                CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("sessionSecret", sessionSecret, "sessionId",
                sessionId, "updateSessionThreedsMethodCompletionIndicatorRequest",
                updateSessionThreedsMethodCompletionIndicatorRequest);
            return UpdateSessionThreedsMethodCompletionIndicator(sessionId,
                updateSessionThreedsMethodCompletionIndicatorRequest,
                SessionSecretAuthorization(sessionSecret), cancellationToken);
        }

        private async Task<GetSessionDetailsResponseOk> GetSessionDetails(string sessionId, SdkAuthorization sdkAuthorization,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams(SessionIdPath, sessionId, "sdkAuthorization", sdkAuthorization);
            return await ApiClient.Get<GetSessionDetailsResponseOk>(BuildPath(SessionsPath, sessionId), sdkAuthorization,
                cancellationToken);
        }

        private async Task<UpdateASessionResponseOk> UpdateASession(string sessionId, AbstractUpdateASessionRequest updateASessionRequest,
            SdkAuthorization sdkAuthorization,
            CancellationToken cancellationToken = default)
        {
            return await ApiClient.Put<UpdateASessionResponseOk>(BuildPath(SessionsPath, sessionId, CollectDataPath),
                sdkAuthorization, updateASessionRequest, cancellationToken);
        }

        private async Task<CompleteASessionResponseNoContent> CompleteASession(string sessionId, SdkAuthorization sdkAuthorization,
            CancellationToken cancellationToken = default)
        {
            return await ApiClient.Post<CompleteASessionResponseNoContent>(BuildPath(SessionsPath, sessionId, CompletePath),
                sdkAuthorization, null,
                cancellationToken, null);
        }

        private async Task<UpdateSessionThreedsMethodCompletionIndicatorResponseOk> UpdateSessionThreedsMethodCompletionIndicator(
            string sessionId, UpdateSessionThreedsMethodCompletionIndicatorRequest updateSessionThreedsMethodCompletionIndicatorRequest,
            SdkAuthorization sdkAuthorization, CancellationToken cancellationToken = default)
        {
            return await ApiClient.Put<UpdateSessionThreedsMethodCompletionIndicatorResponseOk>(
                BuildPath(SessionsPath, sessionId, IssuerFingerprintPath), sdkAuthorization,
                updateSessionThreedsMethodCompletionIndicatorRequest, cancellationToken);
        }

        private static SdkAuthorization SessionSecretAuthorization(string sessionSecret)
        {
            return new SessionSecretSdkCredentials(sessionSecret).GetSdkAuthorization(SdkAuthorizationType.Custom);
        }
    }
}