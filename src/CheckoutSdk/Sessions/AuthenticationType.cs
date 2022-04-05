using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum AuthenticationType
    {
        [EnumMember(Value = "regular")] Regular,
        [EnumMember(Value = "recurring")] Recurring
    }
}