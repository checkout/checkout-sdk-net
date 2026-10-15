using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The properties every Payment Setup payment method shares: its status and the flags that explain it.
    /// </summary>
    public abstract class PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The payment method status.
        /// [Optional]
        /// readOnly
        /// </summary>
        public PaymentMethodStatus? Status { get; set; }

        /// <summary>
        /// The list of error codes or indicators that highlight missing or invalid information.
        /// [Optional]
        /// readOnly
        /// </summary>
        public IList<string> Flags { get; set; }
    }
}
