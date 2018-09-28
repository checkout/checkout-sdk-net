using System.Runtime.Serialization;

namespace Checkout.Payments
{
    /// <summary>
    /// Defines the type of payment action.
    /// </summary>
    public enum ActionType
    {
        Authorization,
        [EnumMember(Value = "Card Verification")]
        CardVerification,
        Void,
        Capture,
        Refund
    }
}
