using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.Common.BillingAddress;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.Common.Identification;
using Phone = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.AccountHolder.Common.Phone.Phone;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.IndividualAccountHolder
{
    /// <summary>
    /// individual account_holder Class
    /// The unreferenced refund destination account holder.
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
        /// The account holder's first name.
        /// This must be a valid legal name. The following formats for the first_name value will return a field
        /// validation error:
        /// a single character
        /// all numeric characters
        /// all punctuation characters
        /// [Required]
        /// <= 50
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The account holder's last name.
        /// This must be a valid legal name. The following formats for the last_name value will return a field
        /// validation error:
        /// a single character
        /// all numeric characters
        /// all punctuation characters
        /// [Required]
        /// <= 50
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The account holder's middle name.
        /// [Optional]
        /// <= 50
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The account holder's billing address.
        /// If your company is incorporated in the United States, this field is required for all unreferenced refunds
        /// you perform.
        /// [Optional]
        /// </summary>
        public BillingAddress BillingAddress { get; set; }

        /// <summary>
        /// The account holder's phone number.
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// The account holder's identification.
        /// Providing identification details for the unreferenced refund recipient increases the likelihood of a
        /// successful unreferenced refund.
        /// [Optional]
        /// </summary>
        public Identification Identification { get; set; }

        /// <summary>
        /// The account holder's email address.
        /// [Optional]
        /// <= 255
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The account holder's date of birth, in the format YYYY-MM-DD.
        /// [Optional]
        /// 10 characters
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// The account holder's country of birth, as a two-letter ISO country code.
        /// [Optional]
        /// 2 characters
        /// </summary>
        public CountryCode? CountryOfBirth { get; set; }
    }
}