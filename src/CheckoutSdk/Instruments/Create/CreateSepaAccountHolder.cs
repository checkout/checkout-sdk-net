namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The account holder details of a SEPA instrument being stored.
    /// </summary>
    public class CreateSepaAccountHolder
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
        public CreateSepaBillingAddress BillingAddress { get; set; }

        /// <summary>
        /// The type of account holder.
        /// [Optional]
        /// </summary>
        public InstrumentAccountHolderType? Type { get; set; }
    }
}
