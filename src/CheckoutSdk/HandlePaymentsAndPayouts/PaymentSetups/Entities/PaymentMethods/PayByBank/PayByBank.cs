using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Pay by Bank (Open Banking) payment method's details and configuration.
    /// </summary>
    public class PayByBank : PaymentMethodBase
    {
        /// <summary>
        /// The identifier of the bank the customer has selected for the payment.
        /// [Optional]
        /// </summary>
        public string BankId { get; set; }

        /// <summary>
        /// The next available action for the payment method.
        /// [Optional] readOnly
        /// </summary>
        public PayByBankAction Action { get; set; }

        /// <summary>
        /// Not part of the PayByBank schema in the Payment Setups API specification. The property is not
        /// nullable, so this SDK always sends it, as "disabled" unless you set it. There is no replacement.
        /// [Optional]
        /// Default: "disabled"
        /// </summary>
        [Obsolete("Not part of the PayByBank schema in the Payment Setups API specification. There is no replacement. Will be removed in the next major version.")]
        public new PaymentMethodInitialization Initialization
        {
            get { return base.Initialization; }
            set { base.Initialization = value; }
        }
    }
}
