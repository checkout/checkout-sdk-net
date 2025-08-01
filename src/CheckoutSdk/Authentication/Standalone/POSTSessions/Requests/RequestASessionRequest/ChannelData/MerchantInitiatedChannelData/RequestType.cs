using System.Runtime.Serialization;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.MerchantInitiatedChannelData
{
    public enum RequestType
    {
        [EnumMember(Value = "recurring_transaction")]
        RecurringTransaction,

        [EnumMember(Value = "installment_transaction")]
        InstallmentTransaction,

        [EnumMember(Value = "add_card")]
        AddCard,

        [EnumMember(Value = "maintain_card_information")]
        MaintainCardInformation,

        [EnumMember(Value = "account_verification")]
        AccountVerification,

        [EnumMember(Value = "split_or_delayed_shipment")]
        SplitOrDelayedShipment,

        [EnumMember(Value = "top_up")]
        TopUp,

        [EnumMember(Value = "mail_order")]
        MailOrder,

        [EnumMember(Value = "telephone_order")]
        TelephoneOrder,

        [EnumMember(Value = "whitelist_status_check")]
        WhitelistStatusCheck,

        [EnumMember(Value = "other_payment")]
        OtherPayment,
    }
}