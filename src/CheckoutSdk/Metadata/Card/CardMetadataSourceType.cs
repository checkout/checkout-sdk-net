using System.Runtime.Serialization;

namespace Checkout.Metadata.Card
{
    public enum CardMetadataSourceType
    {
        [EnumMember(Value = "bin")] Bin,
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "token")] Token,
        [EnumMember(Value = "id")] Id,
    }
}