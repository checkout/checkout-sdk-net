using System.Runtime.Serialization;

namespace Checkout.Payments.Sender
{
    public enum SourceOfFunds
    {
        [EnumMember(Value = "credit")] 
        Credit,

        [EnumMember(Value = "debit")] 
        Debit,

        [EnumMember(Value = "prepaid")] 
        Prepaid,

        [EnumMember(Value = "deposit_account")]
        DepositAccount,

        [EnumMember(Value = "mobile_money_account")]
        MobileMoneyAccount,

        [EnumMember(Value = "cash")] 
        Cash,
    }
}