using System.Runtime.Serialization;

namespace Checkout.Metadata.Card
{
    public enum CardMetadataType
    {
        /// <summary>Credit card.</summary>
        [EnumMember(Value = "credit")]
        Credit,

        /// <summary>Debit card.</summary>
        [EnumMember(Value = "debit")]
        Debit,

        /// <summary>Prepaid card.</summary>
        [EnumMember(Value = "prepaid")]
        Prepaid,

        /// <summary>Charge card.</summary>
        [EnumMember(Value = "charge")]
        Charge,

        /// <summary>Deferred debit card.</summary>
        [EnumMember(Value = "deferred debit")]
        DeferredDebit,

        /// <summary>Network token.</summary>
        [EnumMember(Value = "network token")]
        NetworkToken,
    }
}
