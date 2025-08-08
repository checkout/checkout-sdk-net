using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.KnetSource
{
    public enum LanguageType
    {
        [EnumMember(Value = "ar")]
        Ar,

        [EnumMember(Value = "en")]
        En,
    }
}