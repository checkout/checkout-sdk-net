namespace Checkout.Instruments.Update
{
    /// <summary>
    /// The account holder details of a Bacs Direct Debit instrument being updated.
    /// </summary>
    public class UpdateBacsAccountHolder
    {
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
        /// max 50 characters
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The billing address of the account holder.
        /// [Optional]
        /// </summary>
        public UpdateBacsBillingAddress BillingAddress { get; set; }

        /// <summary>
        /// The type of account holder.
        /// [Optional]
        /// </summary>
        public InstrumentAccountHolderType? Type { get; set; }
    }
}
