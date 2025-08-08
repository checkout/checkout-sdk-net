using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseOk.Processing
{
    public enum PanTypeProcessedType
    {
        [EnumMember(Value = "fpan")]
        Fpan,

        [EnumMember(Value = "dpan")]
        Dpan,
    }
}