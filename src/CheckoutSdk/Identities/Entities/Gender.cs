using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    public enum Gender
    {
        [EnumMember(Value = "M")]
        M,
        [EnumMember(Value = "F")]
        F
    }
}