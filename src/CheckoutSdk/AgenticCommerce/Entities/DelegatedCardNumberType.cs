using System.Runtime.Serialization;

namespace Checkout.AgenticCommerce.Entities
{
    /// <summary>
    /// The type of card number provided in a delegated payment request.
    /// </summary>
    public enum DelegatedCardNumberType
    {
        /// <summary>A Funding Primary Account Number — the card number printed on the card.</summary>
        [EnumMember(Value = "fpan")]
        Fpan,

        /// <summary>A provisioned network token that represents the underlying card.</summary>
        [EnumMember(Value = "network_token")]
        NetworkToken,
    }
}
