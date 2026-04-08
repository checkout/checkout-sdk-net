using System.Runtime.Serialization;

namespace Checkout.Issuing.DigitalCards.Responses
{
    /// <summary>
    /// The digital card's current status.
    /// </summary>
    public enum IssuingDigitalCardStatus
    {
        /// <summary>The default state — authorizations will be declined until the card is activated.</summary>
        [EnumMember(Value = "inactive")]
        Inactive,

        /// <summary>Authorization requests can be approved.</summary>
        [EnumMember(Value = "active")]
        Active,

        /// <summary>Incoming authorization requests will be declined permanently — the card cannot be reactivated.</summary>
        [EnumMember(Value = "deleted")]
        Deleted,
    }
}
