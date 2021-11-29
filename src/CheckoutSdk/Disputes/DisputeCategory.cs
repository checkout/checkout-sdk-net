using System.Runtime.Serialization;

namespace Checkout.Disputes
{
    public enum DisputeCategory
    {
        [EnumMember(Value = "general")] General,
        [EnumMember(Value = "duplicate")] Duplicate,
        [EnumMember(Value = "fraudulent")] Fraudulent,
        [EnumMember(Value = "unrecognized")] Unrecognized,

        [EnumMember(Value = "incorrect_amount")]
        IncorrectAmount,

        [EnumMember(Value = "not_as_described")]
        NotAsDescribed,

        [EnumMember(Value = "credit_not_issued")]
        CreditNotIssued,

        [EnumMember(Value = "canceled_recurring")]
        CanceledRecurring,

        [EnumMember(Value = "product_service_not_received")]
        ProductServiceNotReceived
    }
}