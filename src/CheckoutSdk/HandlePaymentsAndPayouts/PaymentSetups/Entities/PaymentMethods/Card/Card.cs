using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Card payment method's details and configuration.
    /// </summary>
    public class Card : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The Checkout.com card token to use instead of the raw card details.
        /// [Optional]
        /// </summary>
        public CardToken Token { get; set; }

        /// <summary>
        /// The card details.
        /// [Optional]
        /// </summary>
        public CardDetails Details { get; set; }

        /// <summary>
        /// Set to true if you intend to reuse the payment credentials in subsequent payments. If set to false, a
        /// payment instrument will not be included in the payment response.
        /// [Optional]
        /// writeOnly
        /// </summary>
        public bool? StoreForFutureUse { get; set; }

        /// <summary>
        /// The customer's phone number.
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Account holder details for the payment.
        /// [Optional]
        /// </summary>
        public PaymentSetupCardAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The list of supported card scheme names.
        /// A scheme the SDK does not know yet deserializes as null instead of failing the response.
        /// [Optional]
        /// readOnly
        /// </summary>
        public IList<PaymentSetupScheme?> Schemes { get; set; }

        /// <summary>
        /// The preferred card scheme to process the payment.
        /// [Optional]
        /// </summary>
        public PreferredSchema? PreferredScheme { get; set; }
    }
}
