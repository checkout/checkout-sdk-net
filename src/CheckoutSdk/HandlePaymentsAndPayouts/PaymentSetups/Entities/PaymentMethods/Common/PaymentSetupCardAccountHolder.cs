namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// Account holder details for the payment, used by the card, Apple Pay and Google Pay payment methods.
    /// </summary>
    public class PaymentSetupCardAccountHolder
    {
        /// <summary>
        /// The card account holder type.
        /// [Optional]
        /// </summary>
        public PaymentSetupCardAccountHolderType? Type { get; set; }

        /// <summary>
        /// The card account holder's first name.
        /// [Optional]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The card account holder's last name.
        /// [Optional]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The card account holder's middle name.
        /// [Optional]
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The card account holder's company name.
        /// [Optional]
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Indicates whether the ANI (account name inquiry) check is performed with the card scheme (Visa or
        /// Mastercard).
        /// [Optional]
        /// </summary>
        public bool? AccountNameInquiry { get; set; }
    }
}
