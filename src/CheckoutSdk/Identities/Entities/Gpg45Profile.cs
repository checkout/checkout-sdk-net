using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The GPG 45 identity profile the verification meets.
    /// </summary>
    public enum Gpg45Profile
    {
        [EnumMember(Value = "M1A")]
        M1A,

        [EnumMember(Value = "M1C")]
        M1C,

        [EnumMember(Value = "H1A")]
        H1A
    }
}
