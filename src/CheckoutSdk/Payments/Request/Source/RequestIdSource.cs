using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.AccountHolder;

namespace Checkout.Payments.Request.Source
{
    public class RequestIdSource : AbstractRequestSource
    {
        public RequestIdSource() : base(PaymentSourceType.Id)
        {
        }

        /// <summary>
        /// The payment source identifier.
        /// [Required]
        /// ^(src)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The card verification value/code.
        /// [Optional]
        /// ^tok_(\w{26})$|^\d{3,4}$
        /// </summary>
        public string Cvv { get; set; }

        /// <summary>
        /// The payment method. Required for ACH payment.
        /// [Optional]
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Whether the payment uses stored card details.
        /// [Optional]
        /// </summary>
        public bool? Stored { get; set; }

        /// <summary>
        /// Whether to store the credentials for future use.
        /// [Optional]
        /// </summary>
        public bool? StoreForFutureUse { get; set; }

        /// <summary>
        /// The cardholder's billing address.
        /// [Optional]
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The cardholder's phone number.
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Whether to use the Real-Time Account Updater to update the card information.
        /// [Optional]
        /// </summary>
        public bool? AllowUpdate { get; set; }

        /// <summary>
        /// Information about the account holder of the payment instrument.
        /// Valid types: individual (first_name, last_name, middle_name, account_name_inquiry),
        /// corporate (company_name, account_name_inquiry),
        /// government (company_name, account_name_inquiry).
        /// [Optional]
        /// </summary>
        public AbstractAccountHolder AccountHolder { get; set; }
    }
}