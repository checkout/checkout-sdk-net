using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.CardSource
{
    public enum AccountUpdateStatusType
    {
        [EnumMember(Value = "card_updated")]
        CardUpdated,

        [EnumMember(Value = "card_expiry_updated")]
        CardExpiryUpdated,

        [EnumMember(Value = "card_closed")]
        CardClosed,

        [EnumMember(Value = "contact_cardholder")]
        ContactCardholder,

    }
}
