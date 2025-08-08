using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination
{
    public enum DestinationType
    {
        [EnumMember(Value = "token")]
        Token,

        [EnumMember(Value = "id")]
        Id,

        [EnumMember(Value = "card")]
        Card,

        [EnumMember(Value = "network_token")]
        NetworkToken,
    }
}