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
        /// Payment method options specific to Tabby
        /// </summary>
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}
