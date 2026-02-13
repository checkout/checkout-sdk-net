using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.AmlScreening.Responses
{
    public class AmlScreeningResponse : BaseResponse
    {
        /// <summary>
        /// The applicant's unique identifier
        /// [Required]
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// The screening status.
        /// Enum: "created" "screening_in_progress" "approved" "declined" "review_required"
        /// [Required]
        /// </summary>
        public AmlScreeningStatus Status { get; set; }

        /// <summary>
        /// The screening's configuration details
        /// [Required]
        /// </summary>
        public SearchParameters SearchParameters { get; set; }

        /// <summary>
        /// Indicates whether to continue to monitor the applicant's AML status
        /// </summary>
        public bool? Monitored { get; set; }
    }
}