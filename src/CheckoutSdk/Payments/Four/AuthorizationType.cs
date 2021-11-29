using System.Runtime.Serialization;

namespace Checkout.Payments.Four
{
    public enum AuthorizationType
    {
        [EnumMember(Value = "Final")] Final,
        [EnumMember(Value = "Estimated")] Estimated,
        [EnumMember(Value = "Incremental")] Incremental
    }
}