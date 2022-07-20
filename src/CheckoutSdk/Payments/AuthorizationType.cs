using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum AuthorizationType
    {
        [EnumMember(Value = "Final")] Final,
        [EnumMember(Value = "Estimated")] Estimated,
        [EnumMember(Value = "Incremental")] Incremental
    }
}