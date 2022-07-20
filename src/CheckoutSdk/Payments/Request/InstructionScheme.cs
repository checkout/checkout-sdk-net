using System.Runtime.Serialization;

namespace Checkout.Payments.Request
{
    public enum InstructionScheme
    {
        [EnumMember(Value = "swift")] Swift,
        [EnumMember(Value = "local")] Local,
        [EnumMember(Value = "instant")] Instant
    }
}