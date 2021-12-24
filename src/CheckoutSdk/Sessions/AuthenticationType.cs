using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum AuthenticationType
    {
        [EnumMember(Value = "regular")] Regular
    }
}