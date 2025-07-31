using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.Common
{
    public enum TransactionType
    {
        [EnumMember(Value = "goods_service")]
        GoodsService,

        [EnumMember(Value = "check_acceptance")]
        CheckAcceptance,

        [EnumMember(Value = "account_funding")]
        AccountFunding,

        [EnumMember(Value = "quasi_card_transaction")]
        QuasiCardTransaction,

        [EnumMember(Value = "prepaid_activation_and_load")]
        PrepaidActivationAndLoad,
    }
}