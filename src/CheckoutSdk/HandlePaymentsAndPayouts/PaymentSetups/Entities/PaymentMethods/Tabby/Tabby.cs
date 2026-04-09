using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class Tabby : PaymentMethodBase
    {
        /// <summary>
        /// The available payment types for Tabby (for example, installments).
        /// </summary>
        public IList<string> PaymentTypes { get; set; }

        /// <summary>
        /// Payment method options specific to Tabby
        /// </summary>
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}