using System.Runtime.Serialization;

namespace Checkout.Payments
{
    /// <summary>
    /// Specifies which ACH service to use for the payment.
    /// </summary>
    public enum AchServiceType
    {
        [EnumMember(Value = "same_day")]
        SameDay,

        [EnumMember(Value = "standard")]
        Standard
    }
}
