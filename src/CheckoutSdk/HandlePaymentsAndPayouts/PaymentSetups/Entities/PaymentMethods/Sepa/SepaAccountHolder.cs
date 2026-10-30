namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The account holder details.
    /// </summary>
    public class SepaAccountHolder
    {
        /// <summary>
        /// The type of account holder.
        /// [Optional]
        /// </summary>
        public SepaAccountHolderType? Type { get; set; }

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
    }
}
