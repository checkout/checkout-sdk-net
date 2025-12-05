using Checkout.Common;
using Checkout.Payments.Sessions;

using System.Collections.Generic;

using Entities = Checkout.HandlePaymentsAndPayouts.Flow.Entities;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class PaymentMethodConfiguration
    {
        /// <summary>
        /// Configuration options specific to Apple Pay payments.
        /// </summary>
        public ApplePayConfiguration ApplePay { get; set; }

        /// <summary>
        /// Configuration options specific to card payments.
        /// </summary>
        public CardConfiguration Card { get; set; }

        /// <summary>
        /// Configuration options specific to Google Pay payments.
        /// </summary>
        public GooglePayConfiguration GooglePay { get; set; }

        /// <summary>
        /// Configuration options specific to stored card payments.
        /// </summary>
        public StoredCardConfiguration StoredCard { get; set; }
    }

    public class ApplePayConfiguration
    {
        /// <summary>
        /// Specifies whether you intend to store the cardholder's payment details. Default: "disabled"
        /// </summary>
        public StorePaymentDetailsType? StorePaymentDetails { get; set; } = StorePaymentDetailsType.Disabled;

        /// <summary>
        /// The account holder's details.
        /// </summary>
        public AccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The type of the Apple Pay payment total line item. Default: "final"
        /// </summary>
        public TotalType? TotalType { get; set; } = Checkout.HandlePaymentsAndPayouts.Flow.Entities.TotalType.Final;
    }

    public class CardConfiguration
    {
        /// <summary>
        /// Specifies whether you intend to store the cardholder's payment details. Default: "disabled"
        /// </summary>
        public StorePaymentDetailsType? StorePaymentDetails { get; set; } = StorePaymentDetailsType.Disabled;

        /// <summary>
        /// The account holder's details.
        /// </summary>
        public AccountHolder AccountHolder { get; set; }
    }

    public class GooglePayConfiguration
    {
        /// <summary>
        /// Specifies whether you intend to store the cardholder's payment details. Default: "disabled"
        /// </summary>
        public StorePaymentDetailsType? StorePaymentDetails { get; set; } = StorePaymentDetailsType.Disabled;

        /// <summary>
        /// The account holder's details.
        /// </summary>
        public AccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The status of the Google Pay payment total price. Default: "final"
        /// </summary>
        public TotalPriceStatus? TotalPriceStatus { get; set; } = Checkout.HandlePaymentsAndPayouts.Flow.Entities.TotalPriceStatus.Final;
    }

    public class StoredCardConfiguration
    {
        /// <summary>
        /// The unique identifier for an existing customer.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The unique identifiers for card Instruments.
        /// </summary>
        public IList<string> InstrumentIds { get; set; }

        /// <summary>
        /// The unique identifier for the payment instrument to present to the customer as the default option.
        /// </summary>
        public string DefaultInstrumentId { get; set; }
    }

    public class CustomerRetry
    {
        /// <summary>
        /// The maximum number of authorization retry attempts, excluding the initial authorization. Default: 5
        /// </summary>
        public int? MaxAttempts { get; set; } = 5;
    }

    public enum TotalType
    {
        Pending,
        
        Final
    }

    public enum TotalPriceStatus
    {
        Estimated,
        
        Final
    }
}