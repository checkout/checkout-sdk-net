using System.Collections.Generic;
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

        /// <summary>
        /// One or more codes that provide more information about risks associated with the
        /// verification.
        /// </summary>
        /// <remarks>
        /// Not part of the current API specification for ID document verifications: the
        /// id-document-verification response schema does not declare risk_labels, so this property
        /// never populates. Retained for backward compatibility and scheduled for removal in a
        /// future major version. Read risk labels from address document, identity or face
        /// verifications instead.
        /// </remarks>
        public List<RiskLabel> RiskLabels { get; set; }
    }
}