namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The account holder details of a Bacs Direct Debit instrument being stored.
    /// </summary>
    public class CreateBacsAccountHolder
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
        /// The billing address of the account holder.
        /// [Required]
        /// </summary>
        public CreateBacsBillingAddress BillingAddress { get; set; }
    }
}
