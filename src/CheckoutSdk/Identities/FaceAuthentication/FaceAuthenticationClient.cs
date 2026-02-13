using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.FaceAuthentication.Requests;
using Checkout.Identities.FaceAuthentication.Responses;

namespace Checkout.Identities.FaceAuthentication
{
    public class FaceAuthenticationClient : AbstractClient, IFaceAuthenticationClient
    {
        private const string FaceAuthenticationsPath = "face-authentications";
        private const string AnonymizePath = "anonymize";
        private const string AttemptsPath = "attempts";

        public FaceAuthenticationClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        ///     Creates a new face authentication
        /// </summary>
        /// <param name="faceAuthenticationRequest">the face authentication request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication response</returns>
        public Task<FaceAuthenticationResponse> CreateFaceAuthentication(FaceAuthenticationRequest faceAuthenticationRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("faceAuthenticationRequest", faceAuthenticationRequest);
            return ApiClient.Post<FaceAuthenticationResponse>(FaceAuthenticationsPath, 
                SdkAuthorization(), faceAuthenticationRequest, cancellationToken);
        }

        /// <summary>
        ///     Retrieves an existing face authentication by ID
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication response</returns>
        public Task<FaceAuthenticationResponse> GetFaceAuthentication(string faceAuthenticationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("faceAuthenticationId", faceAuthenticationId);
            return ApiClient.Get<FaceAuthenticationResponse>(BuildPath(FaceAuthenticationsPath, faceAuthenticationId), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        /// Anonymizes a face authentication by removing personal data
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication response</returns>
        public Task<FaceAuthenticationResponse> AnonymizeFaceAuthentication(string faceAuthenticationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("faceAuthenticationId", faceAuthenticationId);
            return ApiClient.Post<FaceAuthenticationResponse>(BuildPath(FaceAuthenticationsPath, faceAuthenticationId, AnonymizePath), 
                SdkAuthorization(), (object)null, cancellationToken);
        }

        /// <summary>
        /// Creates a new face authentication attempt
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="faceAuthenticationAttemptRequest">the face authentication attempt request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication attempt response</returns>
        public Task<FaceAuthenticationAttemptResponse> CreateFaceAuthenticationAttempt(string faceAuthenticationId, FaceAuthenticationAttemptRequest faceAuthenticationAttemptRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("faceAuthenticationId", faceAuthenticationId);
            CheckoutUtils.ValidateParams("faceAuthenticationAttemptRequest", faceAuthenticationAttemptRequest);
            return ApiClient.Post<FaceAuthenticationAttemptResponse>(BuildPath(FaceAuthenticationsPath, faceAuthenticationId, AttemptsPath), 
                SdkAuthorization(), faceAuthenticationAttemptRequest, cancellationToken);
        }

        /// <summary>
        /// Retrieves all attempts for a face authentication
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication attempts response</returns>
        public Task<FaceAuthenticationAttemptsResponse> GetFaceAuthenticationAttempts(string faceAuthenticationId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("faceAuthenticationId", faceAuthenticationId);
            return ApiClient.Get<FaceAuthenticationAttemptsResponse>(BuildPath(FaceAuthenticationsPath, faceAuthenticationId, AttemptsPath), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        ///     Retrieves a specific attempt for a face authentication
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication attempt response</returns>
        public Task<FaceAuthenticationAttemptResponse> GetFaceAuthenticationAttempt(string faceAuthenticationId, string attemptId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("faceAuthenticationId", faceAuthenticationId);
            CheckoutUtils.ValidateParams("attemptId", attemptId);
            return ApiClient.Get<FaceAuthenticationAttemptResponse>(BuildPath(FaceAuthenticationsPath, faceAuthenticationId, AttemptsPath, attemptId), 
                SdkAuthorization(), cancellationToken);
        }
    }
}