using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.Common.BillingAddress;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.Common.Identification;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.Common.Phone;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.
    AccountHolder.GovernmentAccountHolder
{
    /// <summary>
    /// government account_holder Class
    /// The unreferenced refund destination account holder.
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
        /// The account holder's company name.
        /// This must be a valid legal name. The following formats for the company_name value will return a field
        /// validation error:
        /// a single character
        /// all numeric characters
        /// all punctuation characters
        /// [Required]
        /// <= 50
        /// </summary>
        public string CompanyName { get; set; }

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
    }
}