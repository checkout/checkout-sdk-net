namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Information about the account holder's details for a Swish payment.
    /// </summary>
    public class SwishAccountHolder
    {
        /// <summary>
        /// The account holder's first name.
        /// [Required]
        /// max 50 characters
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The account holder's last name.
        /// [Required]
        /// max 50 characters
        /// </summary>
        public string LastName { get; set; }
    }
}
