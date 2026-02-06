using System;
using System.Collections.Generic;
using Checkout.Common;
using Checkout.Identities.Common;

namespace Checkout.Identities.IdentityVerification.Responses
{
    public class IdentityVerificationWorkflowResponse : Resource
    {
        public string Id { get; set; }

        public string Reference { get; set; }

        public string ApplicantId { get; set; }

        public VerificationStatus Status { get; set; }

        public ClientInformation ClientInformation { get; set; }

        public VerifiedIdentity VerifiedIdentity { get; set; }

        public List<IdentityVerificationAttemptSummary> AttemptSummaries { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Reason { get; set; }
    }
}