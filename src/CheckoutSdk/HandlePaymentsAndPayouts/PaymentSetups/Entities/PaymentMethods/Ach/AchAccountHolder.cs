namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The account holder details.
    /// </summary>
    public class AchAccountHolder
    {
        /// <summary>
        /// The type of account holder.
        /// [Optional]
        /// </summary>
        public AchAccountHolderType? Type { get; set; }

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
        /// The account holder's date of birth.
        /// [Optional]
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// The account holder's government document identification, for example a Social Security Number
        /// (SSN).
        /// [Optional] writeOnly
        /// </summary>
        public AchAccountHolderIdentification Identification { get; set; }
    }
}
