using System.Runtime.Serialization;

namespace Checkout.Metadata.Card
{
    public enum CardMetadataType
    {
        [EnumMember(Value = "credit")] Credit,
        [EnumMember(Value = "debit")] Debit,
        [EnumMember(Value = "prepaid")] Prepaid,
        [EnumMember(Value = "charge")] Charge,
        [EnumMember(Value = "deferred_debit")] DeferredDebit,
    }
}