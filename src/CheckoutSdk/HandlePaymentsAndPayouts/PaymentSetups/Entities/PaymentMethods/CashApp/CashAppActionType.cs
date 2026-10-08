using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The type of the next available action for the Cash App payment method.
    /// </summary>
    public enum CashAppActionType
    {
        /// <summary>
        /// Redirect the customer to Cash App to authorize the payment.
        /// </summary>
        [EnumMember(Value = "redirect")]
        Redirect
    }
}
