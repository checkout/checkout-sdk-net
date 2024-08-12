using System.Runtime.Serialization;

namespace Checkout.Sessions
{
    public enum TransactionType
    {
        [EnumMember(Value = "account_funding")]
        AccountFunding,

        [EnumMember(Value = "check_acceptance")]
        CheckAcceptance,
        
        [EnumMember(Value = "goods_service")]
        GoodsService,

        [EnumMember(Value = "prepaid_activation_and_load")]
        PrepaidActivationAndLoad,

        [EnumMember(Value = "quashi_card_transaction")]
        QuashiCardTransaction
    }
}