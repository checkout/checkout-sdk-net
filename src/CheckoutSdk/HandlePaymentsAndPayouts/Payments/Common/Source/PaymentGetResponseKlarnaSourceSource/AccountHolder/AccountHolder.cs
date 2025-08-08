namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.PaymentGetResponseKlarnaSourceSource.AccountHolder
{
    /// <summary>
    /// account_holder
    /// object describes payee details
    /// </summary>
    public class AccountHolder
    {
        /// <summary>
        /// First name of the account holder.
        /// [Optional]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the account holder.
        /// [Optional]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Address of the account holder.
        /// [Optional]
        /// </summary>
        public BillingAddress.BillingAddress BillingAddress { get; set; }

        /// <summary>
        /// Email address of the account holder.
        /// [Optional]
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Date of birth of the account holder.
        /// [Optional]
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gender of the account holder.
        /// [Optional]
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Phone number of the account holder.
        /// [Optional]
        /// </summary>
        public Phone.Phone Phone
        {
            get;
            set;
        }
    }
}