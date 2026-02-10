using System;
using System.Collections.Generic;
using Checkout.Common;
using Checkout.Identities.Entities;

namespace Checkout.Identities.IdentityVerification.Requests
{
    public class IdentityVerificationRequest
    {
        /// <summary>
        /// The applicant's unique identifier
        /// [Required]
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// The personal details provided by the applicant
        /// [Required]
        /// </summary>
        public DeclaredData DeclaredData { get; set; }

        /// <summary>
        /// Your configuration ID
        /// </summary>
        public string UserJourneyId { get; set; }
    }
}