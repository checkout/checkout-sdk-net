using System.Runtime.Serialization;

namespace Checkout.Metadata.Card
{
    public enum PayoutsTransactionsType
    {
        [EnumMember(Value = "not_supported")] NotSupported,
        [EnumMember(Value = "standard")] Standard,
        [EnumMember(Value = "fast_funds")] FastFunds,
        [EnumMember(Value = "unknown")] Unknown,
    }
}
