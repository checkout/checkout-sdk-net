using System.Runtime.Serialization;

namespace Checkout.Payments.Request
{
    /// <summary>
    /// The payment scheme type to restrict the payout instruction to.
    /// </summary>
    public enum InstructionScheme
    {
        /// <summary>SWIFT international transfer.</summary>
        [EnumMember(Value = "swift")] Swift,

        /// <summary>Local payment scheme.</summary>
        [EnumMember(Value = "local")] Local,

        /// <summary>Instant payment scheme.</summary>
        [EnumMember(Value = "instant")] Instant
    }
}