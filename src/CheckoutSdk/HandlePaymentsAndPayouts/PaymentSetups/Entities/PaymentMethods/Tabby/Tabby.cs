using System;
using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Tabby payment method's details and configuration.
    /// </summary>
    public class Tabby : PaymentMethodBase
    {
        /// <summary>
        /// The available payment types for Tabby (For example, installments).
        /// [Optional]
        /// </summary>
        public IList<string> PaymentTypes { get; set; }

        /// <summary>
        /// The payment method options.
        /// [Optional]
        /// Deprecated: not part of the API spec; the API no longer sends it and ignores it on requests.
        /// </summary>
        [Obsolete("Not part of the Payment Setups API specification; the API no longer sends it and ignores it on requests. There is no replacement. Will be removed in the next major version.")]
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}
