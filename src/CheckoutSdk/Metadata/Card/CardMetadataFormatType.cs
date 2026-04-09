using System.Runtime.Serialization;

namespace Checkout.Metadata.Card
{
    public enum CardMetadataFormatType
    {
        /// <summary>Standard metadata only. This is the default format.</summary>
        [EnumMember(Value = "basic")]
        Basic,

        /// <summary>Standard metadata plus card payouts eligibility fields.</summary>
        [EnumMember(Value = "card_payouts")]
        CardPayouts,
    }
}
