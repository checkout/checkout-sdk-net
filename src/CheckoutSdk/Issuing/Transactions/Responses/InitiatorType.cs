using System.Runtime.Serialization;

namespace Checkout.Issuing.Transactions.Responses
{
    public enum InitiatorType
    {
        [EnumMember(Value = "cardholder")]
        Cardholder,

        [EnumMember(Value = "merchant")]
        Merchant,

        [EnumMember(Value = "acquirer")]
        Acquirer,

        [EnumMember(Value = "card_network")]
        CardNetwork,

        [EnumMember(Value = "issuer")]
        Issuer
    }
}