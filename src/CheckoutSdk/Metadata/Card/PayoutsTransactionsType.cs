using System.Runtime.Serialization;

namespace Checkout.Metadata.Card
{
    public enum PayoutsTransactionsType
    {
        /// <summary>The card is not eligible for this transaction type.</summary>
        [EnumMember(Value = "not_supported")]
        NotSupported,

        /// <summary>The card is eligible at standard speed.</summary>
        [EnumMember(Value = "standard")]
        Standard,

        /// <summary>The card is eligible for fast funds (near-real-time) payouts.</summary>
        [EnumMember(Value = "fast_funds")]
        FastFunds,

        /// <summary>Eligibility status is unknown.</summary>
        [EnumMember(Value = "unknown")]
        Unknown,
    }
}
