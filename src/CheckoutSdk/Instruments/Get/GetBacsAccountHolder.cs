namespace Checkout.Instruments.Get
{
    /// <summary>
    /// The account holder's details of a stored Bacs Direct Debit instrument.
    /// </summary>
    public class GetBacsAccountHolder
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
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The billing address of the account holder.
        /// [Required]
        /// </summary>
        public GetBacsBillingAddress BillingAddress { get; set; }

        /// <summary>
        /// The type of account holder.
        /// [Optional]
        /// </summary>
        public InstrumentAccountHolderType? Type { get; set; }
    }
}
