using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// A card scheme Google Pay accepts as the preferred scheme.
    /// </summary>
    public enum GooglePayPreferredScheme
    {
        [EnumMember(Value = "visa")]
        Visa,

        [EnumMember(Value = "mastercard")]
        Mastercard
    }
}
