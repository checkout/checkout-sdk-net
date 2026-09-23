using System;
using System.Collections.Generic;
using Checkout.Identities.Entities;
using Newtonsoft.Json;
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
        public IdentityDeclaredData DeclaredData { get; set; }

        /// <summary>
        /// The details of the applicant's identity documents
        /// </summary>
        public List<DocumentDetails> Documents { get; set; }

        /// <summary>
        /// The details of the image of the applicant's face extracted from the video
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "face")]
        public FaceImage FaceImage { get; set; }

        /// <summary>
        /// The details of the applicant's verified identity
        /// </summary>
        public VerifiedIdentity VerifiedIdentity { get; set; }

        /// <summary>
        /// The certifications associated with the identity verification.
        /// [Optional]
        /// </summary>
        public List<Certification> Certifications { get; set; }

        /// <summary>
        /// The version of the verification policy applied. Only returned for certified verifications.
        /// [Optional]
        /// </summary>
        public string VerificationPolicyVersion { get; set; }

        /// <summary>
        /// One or more codes that provide more information about risks associated with the
        /// verification.
        /// [Required]
        /// </summary>
        public List<RiskLabel> RiskLabels { get; set; }
    }
}