namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.EpsSource.AccountHolder
{
    /// <summary>
    /// account_holder
    /// The account holder details
    /// </summary>
    public class AccountHolder
    {

        /// <summary>
        /// The first name of the account holder
        /// [Required]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the account holder
        /// [Required]
        /// </summary>
        public string LastName { get; set; }

    }
}
