namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The account holder details.
    /// </summary>
    public class BacsAccountHolder
    {
        /// <summary>
        /// The type of account holder.
        /// [Optional]
        /// Enum: "individual" "corporate"
        /// </summary>
        public BacsAccountHolderType? Type { get; set; }

        /// <summary>
        /// The first name of the account holder.
        /// [Optional]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the account holder.
        /// [Optional]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The legal name of a registered company that holds the account.
        /// [Optional]
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The email address of the account holder.
        /// [Optional]
        /// </summary>
        public string Email { get; set; }
    }
}
