using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.Applicants.Responses
{
    public class ApplicantResponse : BaseResponse
    {
        /// <summary>
        /// Your reference for the applicant
        /// </summary>
        public string ExternalApplicantId { get; set; }

        /// <summary>
        /// The applicant's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The applicant's full name
        /// </summary>
        public string ExternalApplicantName { get; set; }
    }
}