using System;
using System.Collections.Generic;
using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.FaceAuthentication.Responses
{
    public class FaceAuthenticationResponse : BaseVerificationResponse<FaceAuthenticationStatus>
    {
        /// <summary>
        /// The details of the image of the applicant's face extracted from the video
        /// </summary>
        public FaceImage Face { get; set; }

        /// <summary>
        /// One or more codes that provide more information about risks associated with the
        /// verification.
        /// [Required]
        /// </summary>
        public List<RiskLabel> RiskLabels { get; set; }
    }
}