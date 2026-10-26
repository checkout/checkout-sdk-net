using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Stablecoin payment method's details and configuration.
    /// </summary>
    public class Stablecoin : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// Not part of the Stablecoin schema in the Payment Setups API specification; the API ignores it on
        /// requests. The property is not nullable, so this SDK always sends it, as "disabled" unless you set it.
        /// There is no replacement.
        /// [Optional]
        /// Default: "disabled"
        /// </summary>
        [Obsolete("Not part of the Stablecoin schema in the Payment Setups API specification; the API ignores it on requests. There is no replacement. Will be removed in the next major version.")]
        public PaymentMethodInitialization Initialization { get; set; } = PaymentMethodInitialization.Disabled;
    }
}
