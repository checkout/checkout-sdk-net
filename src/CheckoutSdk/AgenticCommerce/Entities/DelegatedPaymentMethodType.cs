using System.Runtime.Serialization;

namespace Checkout.AgenticCommerce.Entities
{
    /// <summary>
    /// The payment method type for a delegated payment.
    /// </summary>
    public enum DelegatedPaymentMethodType
    {
        /// <summary>Card payment method.</summary>
        [EnumMember(Value = "card")] Card,
    }
}
