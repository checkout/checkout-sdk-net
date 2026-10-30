using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Bizum payment method's details and configuration.
    /// </summary>
    public class Bizum : PaymentMethodBase
    {
        /// <summary>
        /// Not part of the Bizum schema in the Payment Setups API specification, which removed it. The property
        /// is not nullable, so this SDK always sends it, as "disabled" unless you set it. There is no replacement.
        /// [Optional]
        /// Default: "disabled"
        /// </summary>
        [Obsolete("Not part of the Bizum schema in the Payment Setups API specification. There is no replacement. Will be removed in the next major version.")]
        public new PaymentMethodInitialization Initialization
        {
            get { return base.Initialization; }
            set { base.Initialization = value; }
        }

        /// <summary>
        /// Not part of the Bizum schema in the Payment Setups API specification. There is no replacement.
        /// [Optional]
        /// </summary>
        [Obsolete("Not part of the Bizum schema in the Payment Setups API specification. There is no replacement. Will be removed in the next major version.")]
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}
