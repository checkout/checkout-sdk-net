using System;
using Checkout.Common;
using Checkout.Identities.Entities;

namespace Checkout.Identities.Applicants.Responses
{
    public class ApplicantResponse : Resource
    {
        /// <summary>
        /// The applicant's unique identifier
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
        /// Your reference for the applicant
        /// </summary>
        public string ExternalApplicantId { get; set; }

        /// <summary>
        /// The applicant's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The applicant's full name
        /// </summary>
        public string ExternalApplicantName { get; set; }
    }
}