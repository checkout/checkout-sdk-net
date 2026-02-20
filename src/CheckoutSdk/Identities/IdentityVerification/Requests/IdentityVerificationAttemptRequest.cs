using System;
using System.Collections.Generic;
using Checkout.Common;
using Checkout.Identities.Entities;

namespace Checkout.Identities.IdentityVerification.Requests
{
    public class IdentityVerificationAttemptRequest
    {
        /// <summary>
        /// The URL to redirect the applicant to after the attempt.
        /// [Required]
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The applicant's details.
        /// </summary>
        public ClientInformation ClientInformation { get; set; }

    }
}