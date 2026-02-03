using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.AccountHolder.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.AccountHolder.GovernmentAccountHolder
{
    /// <summary>
    /// government account_holder Class
    /// Information about the account holder of the card
    /// </summary>
    public class GovernmentAccountHolder : AbstractAccountHolder
    {
        /// <summary>
        /// Initializes a new instance of the GovernmentAccountHolder class.
        /// </summary>
        public GovernmentAccountHolder() : base(AccountHolderType.Government)
        {
        }

        /// <summary>
        /// The card account holder's company name
        /// A valid and legal name must be populated in this field. The populated value cannot be only one character or
        /// all numeric.
        /// [Required]
        /// &lt;= 35
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The Account Name Inquiry check result.
        /// [Required]
        /// </summary>
        public AccountNameInquiryType? AccountNameInquiry { get; set; }
    }
}