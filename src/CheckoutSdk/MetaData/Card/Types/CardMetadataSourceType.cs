using System.Runtime.Serialization;

namespace Checkout.Metadata.Card.Types
{
    public enum CardMetadataSourceType
    {
        [EnumMember(Value = "bin")] Bin,
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "token")] Token,
        [EnumMember(Value = "id")] Id,
    }
}