using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    public class Klarna
    {
        /// <summary>
        /// The status of the Klarna payment method
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the Klarna payment method
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        ///  Default: "disabled"
        /// The initialization state of the payment method.
        /// When you create a Payment Setup, this defaults to disabled.
        /// Enum: "disabled" "enabled"
        /// </summary>
        public KlarnaInitialization Initialization { get; set; } = KlarnaInitialization.Disabled;

        /// <summary>
        /// The account holder information for Klarna payments
        /// </summary>
        public KlarnaAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// Payment method options specific to Klarna
        /// </summary>
        public KlarnaPaymentMethodOptions PaymentMethodOptions { get; set; }
    }

    public enum KlarnaInitialization
    {
        /// <summary>
        /// The Klarna payment method is disabled
        /// </summary>
         [EnumMember(Value = "disabled")]
        Disabled,
        
        /// <summary>
        /// The Klarna payment method is enabled
        /// </summary>
        [EnumMember(Value = "enabled")]
        Enabled
    }
}