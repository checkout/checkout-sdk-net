using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The user action for the PayPal widget.
    /// </summary>
    public enum PaypalUserAction
    {
        /// <summary>
        /// After the customer clicks the PayPal button, the customer is redirected to a page to enter payment
        /// details and immediately directed to finalize the payment.
        /// </summary>
        [EnumMember(Value = "pay_now")]
        PayNow,

        /// <summary>
        /// After the customer clicks the PayPal button, the customer is redirected to a page to enter payment
        /// details and then redirected back to the merchant's site to review and finalize the payment.
        /// </summary>
        [EnumMember(Value = "continue")]
        Continue
    }
}
