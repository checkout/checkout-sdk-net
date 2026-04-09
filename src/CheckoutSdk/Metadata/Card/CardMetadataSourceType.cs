using System.Runtime.Serialization;

namespace Checkout.Metadata.Card
{
    public enum CardMetadataSourceType
    {
        /// <summary>Bank Identification Number source.</summary>
        [EnumMember(Value = "bin")]
        Bin,

        /// <summary>Full card number (PAN) source.</summary>
        [EnumMember(Value = "card")]
        Card,

        /// <summary>Checkout.com payment token source.</summary>
        [EnumMember(Value = "token")]
        Token,

        /// <summary>Stored instrument ID source.</summary>
        [EnumMember(Value = "id")]
        Id,
    }
}
