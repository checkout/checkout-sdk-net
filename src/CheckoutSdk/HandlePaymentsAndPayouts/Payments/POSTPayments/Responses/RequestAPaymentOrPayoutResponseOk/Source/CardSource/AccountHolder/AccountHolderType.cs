using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponse201.Source.CardSource.AccountHolder
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
