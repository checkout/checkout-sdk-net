using System.Runtime.Serialization;

namespace Checkout.Transfers
{
    public enum TransferStatus
    {
        [EnumMember(Value = "pending")] Pending,
        [EnumMember(Value = "completed")] Completed,
        [EnumMember(Value = "rejected")] Rejected
    }
}