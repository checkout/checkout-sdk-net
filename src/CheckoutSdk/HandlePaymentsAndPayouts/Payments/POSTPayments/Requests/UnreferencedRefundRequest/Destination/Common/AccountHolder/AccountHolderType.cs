using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.AccountHolder
{
    public enum AccountHolderType
    {
        [EnumMember(Value = "individual")]
        Individual,

        [EnumMember(Value = "corporate")]
        Corporate,

        [EnumMember(Value = "government")]
        Government,
    }
}