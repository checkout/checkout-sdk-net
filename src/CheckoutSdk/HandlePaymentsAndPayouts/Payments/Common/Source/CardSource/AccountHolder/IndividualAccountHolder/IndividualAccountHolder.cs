using Checkout.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.AccountHolder.IndividualAccountHolder
{
    /// <summary>
    /// individual account_holder Class
    /// Information about the account holder of the card
    /// </summary>
    public class IndividualAccountHolder : AbstractAccountHolder
    {
        /// <summary>
        /// Initializes a new instance of the IndividualAccountHolder class.
        /// </summary>
        public IndividualAccountHolder() : base(AccountHolderType.Individual)
        {
        }

        /// <summary>
        /// The card account holder's first name
        /// A valid and legal name must be populated in this field. The populated value cannot be only one character or
        /// all numeric.
        /// [Required]
        /// &lt;= 35
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The card account holder's last name
        /// A valid and legal name must be populated in this field. The populated value cannot be only one character or
        /// all numeric.
        /// [Required]
        /// &lt;= 35
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The result of the Account Name Inquiry check.
        /// [Required]
        /// </summary>
        public AccountNameInquiryType? AccountNameInquiry { get; set; }

        /// <summary>
        /// The card account holder's middle name
        /// Conditional - required when the card metadata issuer_country = ZA (South Africa)
        /// [Optional]
        /// &lt;= 35
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Details of the Account Name Inquiry check.
        /// [Optional]
        /// </summary>
        public AccountNameInquiryDetails.AccountNameInquiryDetails AccountNameInquiryDetails { get; set; }
    }
}