using Checkout.Identities.Entities;

namespace Checkout.Identities.AmlScreening.Requests
{
    public class AmlScreeningRequest
    {
        /// <summary>
        /// The applicant's unique identifier (Required, ^aplt_\w+$ max 255 characters)
        /// [Required]
        /// </summary>
        public string ApplicantId { get; set; }

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