using System.Runtime.Serialization;

namespace Checkout.Sessions.Channel
{
    public enum RequestType
    {
        [EnumMember(Value = "account_verification")]
        AccountVerification,
        
        [EnumMember(Value = "add_card")] 
        AddCard,

        [EnumMember(Value = "installment_transaction")]
        InstallmentTransaction,
        
        [EnumMember(Value = "mail_order")] 
        MailOrder,

        [EnumMember(Value = "maintain_card_information")]
        MaintainCardInformation,
        
        [EnumMember(Value = "other_payment")] 
        OtherPayment,

        [EnumMember(Value = "recurring_transaction")]
        RecurringTransaction,

        [EnumMember(Value = "split_or_delayed_shipment")]
        SplitOrDelayedShipment,

        [EnumMember(Value = "telephone_order")]
        TelephoneOrder,
        
        [EnumMember(Value = "top_up")] 
        TopUp,

        [EnumMember(Value = "whitelist_status_check")]
        WhitelistStatusCheck
    }
}