using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.NetworkTokenDestination
{
    public enum TokenType
    {
        [EnumMember(Value = "vts")]
        Vts,

        [EnumMember(Value = "mdes")]
        Mdes,

        [EnumMember(Value = "applepay")]
        Applepay,

        [EnumMember(Value = "googlepay")]
        Googlepay,
    }
}