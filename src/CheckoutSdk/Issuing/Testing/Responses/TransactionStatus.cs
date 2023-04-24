using System.Runtime.Serialization;

namespace Checkout.Issuing.Testing.Responses
{
    public enum TransactionStatus
    {
        [EnumMember(Value = "Authorized")] Authorized,
        [EnumMember(Value = "Declined")] Declined
    }
}