using System.Runtime.Serialization;

namespace Checkout.Metadata.Card.Types
{
    public enum CardMetadataFormatType
    {
        [EnumMember(Value = "basic")] Basic,
        [EnumMember(Value = "card_payouts")] CardPayouts,
    }
}