using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.IdDocumentVerification.Responses
{
    public class IdDocumentVerificationResponse : BaseVerificationResponse<IdDocumentVerificationStatus>
    {
        /// <summary>
        /// The personal details provided by the applicant
        /// </summary>
        public DeclaredData DeclaredData { get; set; }

        /// <summary>
        /// The applicant's identity document details
        /// </summary>
        public DocumentDetails Document { get; set; }
    }
}