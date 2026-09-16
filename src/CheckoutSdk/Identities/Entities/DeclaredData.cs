namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The personal details provided by the applicant.
    /// </summary>
    public class DeclaredData
    {
        /// <summary>
        /// The applicant's name.
        /// [Required]
        /// min 2 characters, max 255 characters
        /// Example: Hannah Bret
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The applicant's birth date, as provided by the applicant. This is the declared value,
        /// not the value extracted from the verified document.
        /// [Optional]
        /// Format: date (YYYY-MM-DD)
        /// Example: 1994-10-15
        /// </summary>
        public string BirthDate { get; set; }
    }
}
