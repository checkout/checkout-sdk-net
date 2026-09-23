namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The personal details provided by the applicant for an identity verification.
    /// </summary>
    public class IdentityDeclaredData : DeclaredData
    {
        /// <summary>
        /// The applicant's mobile phone number, if sharing the attempt URL via SMS.
        /// [Optional]
        /// </summary>
        public PhoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// The applicant's email address.
        /// [Optional]
        /// Format: email
        /// Example: hannah.bret@example.com
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The applicant's address.
        /// [Optional]
        /// </summary>
        public IdvAddress Address { get; set; }
    }
}
