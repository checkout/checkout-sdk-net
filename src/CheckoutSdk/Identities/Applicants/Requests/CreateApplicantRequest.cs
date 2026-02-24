using System;
using Checkout.Common;
using Checkout.Identities.Entities;

namespace Checkout.Identities.Applicants.Requests
{
    public class CreateApplicantRequest
    {
        /// <summary>
        /// Your reference for the applicant
        /// </summary>
        public string ExternalApplicantId { get; set; }

        /// <summary>
        /// The applicant's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The applicant's full name (&lt;= 255 characters)
        /// </summary>
        public string ExternalApplicantName { get; set; }
    }
}