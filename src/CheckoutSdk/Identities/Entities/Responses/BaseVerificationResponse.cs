using System.Collections.Generic;

namespace Checkout.Identities.Entities.Responses
{
    /// <summary>
    /// Base class for verification responses
    /// </summary>
    public abstract class BaseVerificationResponse<TStatus> : BaseWithCodesResponse
    {
        /// <summary>
        /// Your configuration ID
        /// [Required]
        /// </summary>
        public string UserJourneyId { get; set; }
        
        /// <summary>
        /// The applicant's unique identifier
        /// [Required]
        /// </summary>
        public string ApplicantId { get; set; }
        
        /// <summary>
        /// The verification status
        /// [Required]
        /// </summary>
        public TStatus Status { get; set; }
        
        /// <summary>
        /// One or more codes that provide more information about risks associated with the verification
        /// [Required]
        /// </summary>
        public List<string> RiskLabels { get; set; }
    }
}