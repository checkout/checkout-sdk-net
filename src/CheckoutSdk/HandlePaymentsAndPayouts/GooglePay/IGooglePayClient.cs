using Checkout.HandlePaymentsAndPayouts.GooglePay.Requests;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay
{
    public interface IGooglePayClient
    {
        /// <summary>
        /// Enroll an entity with Google Pay.
        /// </summary>
        /// <param name="request">The enrollment request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<GooglePayEnrollmentResponse> CreateEnrollment(GooglePayEnrollmentRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Register a web domain for an actively enrolled Google Pay entity.
        /// </summary>
        /// <param name="entityId">The entity identifier</param>
        /// <param name="request">The domain registration request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<EmptyResponse> RegisterDomain(string entityId, GooglePayRegisterDomainRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the list of registered web domains for an enrolled Google Pay entity.
        /// </summary>
        /// <param name="entityId">The entity identifier</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<GooglePayDomainListResponse> GetDomains(string entityId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the current Google Pay enrollment state for an entity.
        /// </summary>
        /// <param name="entityId">The entity identifier</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation</returns>
        Task<GooglePayEnrollmentStateResponse> GetEnrollmentState(string entityId,
            CancellationToken cancellationToken = default);
    }
}
