using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    /// <summary>
    /// The level of confidence in the verified identity.
    /// </summary>
    public enum LevelOfConfidence
    {
        [EnumMember(Value = "medium")]
        Medium,

        [EnumMember(Value = "high")]
        High
    }
}
