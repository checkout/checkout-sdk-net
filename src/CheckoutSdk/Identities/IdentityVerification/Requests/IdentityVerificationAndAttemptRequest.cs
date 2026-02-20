using System.Collections.Generic;
using Checkout.Identities.Entities;
using Newtonsoft.Json;

namespace Checkout.Identities.IdentityVerification.Requests
{
    /// <summary>
    /// Request for creating an identity verification with an initial attempt
    /// </summary>
    public class IdentityVerificationAndAttemptRequest
    {
        /// <summary>
        /// The personal details provided by the applicant
        /// </summary>
        public DeclaredData DeclaredData { get; set; }

        /// <summary>
        /// The URL to redirect the applicant to after the attempt
        /// [Required]
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Your configuration ID
        /// </summary>
        public string UserJourneyId { get; set; }

        /// <summary>
        /// The applicant's unique identifier
        /// </summary>
        public string ApplicantId { get; set; }
    }
}