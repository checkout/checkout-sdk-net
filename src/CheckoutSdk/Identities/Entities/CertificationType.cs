using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The certification type.
    /// </summary>
    public enum CertificationType
    {
        [EnumMember(Value = "diatf")]
        Diatf
    }
}
