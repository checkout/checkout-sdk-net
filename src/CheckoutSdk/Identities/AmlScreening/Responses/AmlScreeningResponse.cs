using System;
using Checkout.Common;
using Checkout.Identities.Entities;

namespace Checkout.Identities.AmlScreening.Responses
{
    public class AmlScreeningResponse : Resource
    {
        /// <summary>
        /// The applicant's unique identifier
        /// [Required]
        /// </summary>
        public string ApplicantId { get; set; }
        
        /// <summary>
        /// The AML screening's unique identifier
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The date and time when the resource was created, in UTC.
        /// Format – yyyy-mm-ddThh:mm:ss.sss
        /// [Required]
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The date and time when the resource was modified, in UTC.
        /// Format – yyyy-mm-ddThh:mm:ss.sss
        /// [Required]
        /// </summary>
        public DateTime ModifiedOn { get; set; }

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