using Checkout.Common;

namespace Checkout.Identities.Entities
{
    public class VerifiedIdentity
    {
        /// <summary>
        /// The applicant's full name
        /// [Required]
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The applicant's birth date.
        /// Format YYYY-MM-DD
        /// [Required]
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// The applicant's first names
        /// </summary>
        public string FirstNames { get; set; }

        /// <summary>
        /// The applicant's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The applicant's last name at birth
        /// </summary>
        public string LastNameAtBirth { get; set; }

        /// <summary>
        /// The applicant's birth place
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// The applicant's nationality.
        /// Standard – ISO alpha-2 country code
        /// Example – FR
        /// </summary>
        public CountryCode? Nationality { get; set; }

        /// <summary>
        /// The applicant's gender.
        /// Enum: "M" "F"
        /// </summary>
        public Gender? Gender { get; set; }
    }
}