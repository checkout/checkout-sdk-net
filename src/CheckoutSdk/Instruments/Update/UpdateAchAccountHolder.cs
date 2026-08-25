namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The account holder details of an ACH instrument being updated.
    /// </summary>
    public class UpdateAchAccountHolder
    {
        /// <summary>
        /// The first name of the account holder.
        /// [Required]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the account holder.
        /// [Required]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The legal name of a registered company that holds the account.
        /// [Required]
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The type of account holder.
        /// [Required]
        /// </summary>
        public InstrumentAccountHolderType? Type { get; set; }
    }
}
