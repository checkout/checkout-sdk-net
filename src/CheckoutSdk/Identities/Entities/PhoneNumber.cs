namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The applicant's mobile phone number, if sharing the attempt URL via SMS.
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// The international phone country code.
        /// [Required]
        /// Pattern: ^\+(\d+)$
        /// Example: +33
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The applicant's mobile number, without the country code.
        /// [Required]
        /// Pattern: ^\d{1,14}$
        /// Example: 5555550102
        /// </summary>
        public string Number { get; set; }
    }
}
