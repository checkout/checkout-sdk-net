using System.Runtime.Serialization;

namespace Checkout.AgenticCommerce.Entities
{
    /// <summary>
    /// The reason for the spending allowance on a delegated payment token.
    /// </summary>
    public enum DelegatedPaymentAllowanceReason
    {
        [EnumMember(Value = "one_time")] OneTime,
    }
}
