using System.Runtime.Serialization;

namespace Checkout.Issuing.Testing.Requests
{
    public enum TransactionType
    {
        [EnumMember(Value = "purchase")] Purchase
    }
}