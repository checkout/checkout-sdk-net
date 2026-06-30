using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The amount and currency to present to the customer, when the settlement currency differs from the
    /// customer-facing currency.
    /// </summary>
    public class PaymentSetupPresentmentDetails
    {
        /// <summary>
        /// The presentment amount, in the minor currency unit.
        /// [Optional]
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The presentment currency, as a three-letter ISO currency code.
        /// [Optional]
        /// </summary>
        public Currency? Currency { get; set; }
    }
}
