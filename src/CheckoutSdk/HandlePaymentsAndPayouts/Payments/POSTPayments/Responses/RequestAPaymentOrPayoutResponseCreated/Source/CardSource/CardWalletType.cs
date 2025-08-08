using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Source.
    CardSource
{
    public enum CardWalletType
    {
        [EnumMember(Value = "applepay")]
        Applepay,

        [EnumMember(Value = "googlepay")]
        Googlepay,
    }
}