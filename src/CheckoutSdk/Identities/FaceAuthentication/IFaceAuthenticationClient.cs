using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.FaceAuthentication.Requests;
using Checkout.Identities.FaceAuthentication.Responses;

namespace Checkout.Identities.FaceAuthentication
{
    /// <summary>
    ///     Client for managing face authentication in identity verification processes
    /// </summary>
    public interface IFaceAuthenticationClient
    {
        /// <summary>
        ///     Creates a new face authentication
        /// </summary>
        /// <param name="faceAuthenticationRequest">the face authentication request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication response</returns>
        Task<FaceAuthenticationResponse> CreateFaceAuthentication(FaceAuthenticationRequest faceAuthenticationRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves an existing face authentication by ID
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication response</returns>
        Task<FaceAuthenticationResponse> GetFaceAuthentication(string faceAuthenticationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Anonymizes a face authentication by removing personal data
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication response</returns>
        Task<FaceAuthenticationResponse> AnonymizeFaceAuthentication(string faceAuthenticationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Creates a new face authentication attempt
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="faceAuthenticationAttemptRequest">the face authentication attempt request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication attempt response</returns>
        Task<FaceAuthenticationAttemptResponse> CreateFaceAuthenticationAttempt(string faceAuthenticationId, FaceAuthenticationAttemptRequest faceAuthenticationAttemptRequest, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves all attempts for a face authentication
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication attempts response</returns>
        Task<FaceAuthenticationAttemptsResponse> GetFaceAuthenticationAttempts(string faceAuthenticationId, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Retrieves a specific attempt for a face authentication
        /// </summary>
        /// <param name="faceAuthenticationId">the face authentication ID</param>
        /// <param name="attemptId">the attempt ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the face authentication attempt response</returns>
        Task<FaceAuthenticationAttemptResponse> GetFaceAuthenticationAttempt(string faceAuthenticationId, string attemptId, CancellationToken cancellationToken = default);
    }
}