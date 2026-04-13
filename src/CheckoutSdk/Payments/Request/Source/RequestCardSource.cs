using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.AccountHolder;

namespace Checkout.Payments.Request.Source
{
    public class RequestCardSource : AbstractRequestSource
    {
        public RequestCardSource() : base(PaymentSourceType.Card)
        {
        }

        /// <summary>
        /// The card number (without separators).
        /// [Required]
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The expiry month of the card.
        /// [Required]
        /// >= 1, <= 12
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The 4-digit expiry year of the card.
        /// [Required]
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The name of the cardholder.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The card verification value/code. 3 digits, except for American Express (4 digits).
        /// [Optional]
        /// >= 3 characters, <= 4 characters
        /// </summary>
        public string Cvv { get; set; }

        /// <summary>
        /// Whether this payment uses stored card details.
        /// [Optional]
        /// </summary>
        public bool? Stored { get; set; }

        /// <summary>
        /// Whether to store the credentials for future use.
        /// [Optional]
        /// </summary>
        public bool? StoreForFutureUse { get; set; }

        /// <summary>
        /// The billing address of the cardholder.
        /// [Optional]
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The phone number of the cardholder.
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Whether to use the Real-Time Account Updater to update the card information.
        /// [Optional]
        /// </summary>
        public bool? AllowUpdate { get; set; }

        /// <summary>
        /// Information about the account holder of the card.
        /// Valid types: individual (first_name, last_name, middle_name, account_name_inquiry),
        /// corporate (company_name, account_name_inquiry),
        /// government (company_name, account_name_inquiry).
        /// [Optional]
        /// </summary>
        public AbstractAccountHolder AccountHolder { get; set; }
    }
}