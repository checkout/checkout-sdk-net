using System;
using System.Collections.Generic;
using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.IdentityVerification.Responses
{
    /// <summary>
    /// Response for identity verification operations
    /// </summary>
    public class IdentityVerificationResponse : BaseVerificationResponse<IdentityVerificationStatus>
    {
        /// <summary>
        /// The personal details provided by the applicant
        /// [Required]
        /// </summary>
        public DeclaredData DeclaredData { get; set; }

        /// <summary>
        /// The details of the applicant's identity documents
        /// </summary>
        public List<DocumentDetails> Documents { get; set; }

        /// <summary>
        /// The details of the image of the applicant's face extracted from the video
        /// </summary>
        public FaceImage FaceImage { get; set; }

        /// <summary>
        /// The details of the applicant's verified identity
        /// </summary>
        public VerifiedIdentity VerifiedIdentity { get; set; }
    }
}