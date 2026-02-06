using System;
using Checkout.Common;
using Checkout.Identities.Common;

namespace Checkout.Identities.Applicants.Requests
{
    public class CreateApplicantRequest
    {
        /// <summary>
        /// External applicant ID (Optional, max 255 characters, alphanumeric with hyphens and underscores only)
        /// </summary>
        public string ExternalApplicantId { get; set; }

        /// <summary>
        /// Applicant email address (Required, max 255 characters)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Applicant first name (Optional, max 255 characters)
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Applicant last name (Optional, max 255 characters)
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth (Optional)
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Place of birth (Optional)
        /// </summary>
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Gender (Optional)
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Nationality country code (Optional)
        /// </summary>
        public CountryCode? Nationality { get; set; }

        /// <summary>
        /// Phone number (Optional)
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Address (Optional)
        /// </summary>
        public Address Address { get; set; }
    }
}