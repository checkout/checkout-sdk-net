using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.Applicants.Requests;
using Checkout.Identities.Applicants.Responses;

namespace Checkout.Identities.Applicants
{
    /// <summary>
    ///     Client for managing applicants in identity verification processes
    /// </summary>
    public interface IApplicantsClient
    {
        /// <summary>
        /// Creates a new applicant
        /// </summary>
        /// <param name="createApplicantRequest">the create applicant request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the created applicant response</returns>
        Task<ApplicantResponse> CreateApplicant(CreateApplicantRequest createApplicantRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an existing applicant by ID
        /// </summary>
        /// <param name="applicantId">the applicant ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the applicant response</returns>
        Task<ApplicantResponse> GetApplicant(string applicantId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing applicant
        /// </summary>
        /// <param name="applicantId">the applicant ID</param>
        /// <param name="updateApplicantRequest">the update applicant request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the updated applicant response</returns>
        Task<ApplicantResponse> UpdateApplicant(string applicantId, UpdateApplicantRequest updateApplicantRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Anonymizes an existing applicant
        /// </summary>
        /// <param name="applicantId">the applicant ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the anonymized applicant response</returns>
        Task<ApplicantResponse> AnonymizeApplicant(string applicantId, CancellationToken cancellationToken = default);
    }
}