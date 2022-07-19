using System.Runtime.Serialization;

namespace Checkout.Transfers
{
    public enum TransferType
    {
        [EnumMember(Value = "commission")] Commission,
        [EnumMember(Value = "promotion")] Promotion,
        [EnumMember(Value = "refund")] Refund,
    }
}