using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.
    CardSource
{
    public enum CardCategoryType
    {
        [EnumMember(Value = "CONSUMER")]
        CONSUMER,

        [EnumMember(Value = "COMMERCIAL")]
        COMMERCIAL,
    }
}