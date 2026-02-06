using System;
using Checkout.Identities.Common;

namespace Checkout.Identities.IdentityVerification.Requests
{
    public class IdentityVerificationSearchRequest
    {
        /// <summary>
        /// Filter by applicant ID (Optional)
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// Filter by workflow ID (Optional)
        /// </summary>
        public string WorkflowId { get; set; }

        /// <summary>
        /// Filter by verification status (Optional)
        /// </summary>
        public VerificationStatus? Status { get; set; }

        /// <summary>
        /// Filter by reference (Optional)
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Filter verifications created after this date (Optional)
        /// </summary>
        public DateTime? CreatedAfter { get; set; }

        /// <summary>
        /// Filter verifications created before this date (Optional)
        /// </summary>
        public DateTime? CreatedBefore { get; set; }

        /// <summary>
        /// Filter verifications updated after this date (Optional)
        /// </summary>
        public DateTime? UpdatedAfter { get; set; }

        /// <summary>
        /// Filter verifications updated before this date (Optional)
        /// </summary>
        public DateTime? UpdatedBefore { get; set; }

        /// <summary>
        /// Maximum number of results to return (Optional, 1-100, default 10)
        /// </summary>
        public int? Limit { get; set; } = 10;

        /// <summary>
        /// Number of results to skip (Optional, default 0)
        /// </summary>
        public int? Skip { get; set; } = 0;
    }
}