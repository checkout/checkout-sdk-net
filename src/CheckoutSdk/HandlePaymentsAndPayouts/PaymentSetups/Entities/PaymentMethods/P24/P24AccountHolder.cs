namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The account holder's details.
    /// </summary>
    public class P24AccountHolder
    {
        /// <summary>
        /// The account holder's name.
        /// [Optional]
        /// min 3 characters, max 100 characters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The account holder's email address.
        /// [Optional]
        /// Format: email
        /// max 254 characters
        /// </summary>
        public string Email { get; set; }
    }
}
