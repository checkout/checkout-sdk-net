using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.Applicants.Requests;
using Checkout.Identities.Applicants.Responses;

namespace Checkout.Identities.Applicants
{
    public class ApplicantsClient : AbstractClient, IApplicantsClient
    {
        private const string ApplicantsPath = "applicants";
        private const string AnonymizePath = "anonymize";

        public ApplicantsClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        ///     Creates a new applicant
        /// </summary>
        /// <param name="createApplicantRequest">the create applicant request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the created applicant response</returns>
        public Task<ApplicantResponse> CreateApplicant(CreateApplicantRequest createApplicantRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("createApplicantRequest", createApplicantRequest);
            return ApiClient.Post<ApplicantResponse>(ApplicantsPath, 
                SdkAuthorization(), createApplicantRequest, cancellationToken);
        }

        /// <summary>
        ///     Retrieves an existing applicant by ID
        /// </summary>
        /// <param name="applicantId">the applicant ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the applicant response</returns>
        public Task<ApplicantResponse> GetApplicant(string applicantId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("applicantId", applicantId);
            return ApiClient.Get<ApplicantResponse>(BuildPath(ApplicantsPath, applicantId), 
                SdkAuthorization(), cancellationToken);
        }

        /// <summary>
        ///     Updates an existing applicant
        /// </summary>
        /// <param name="applicantId">the applicant ID</param>
        /// <param name="updateApplicantRequest">the update applicant request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the updated applicant response</returns>
        public Task<ApplicantResponse> UpdateApplicant(string applicantId, UpdateApplicantRequest updateApplicantRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("applicantId", applicantId, "updateApplicantRequest", updateApplicantRequest);
            return ApiClient.Patch<ApplicantResponse>(BuildPath(ApplicantsPath, applicantId), 
                SdkAuthorization(), updateApplicantRequest, cancellationToken);
        }

        /// <summary>
        ///     Anonymizes an existing applicant
        /// </summary>
        /// <param name="applicantId">the applicant ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the anonymized applicant response</returns>
        public Task<ApplicantResponse> AnonymizeApplicant(string applicantId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("applicantId", applicantId);
            return ApiClient.Post<ApplicantResponse>(BuildPath(ApplicantsPath, applicantId, AnonymizePath), 
                SdkAuthorization(), cancellationToken);
        }
    }
}