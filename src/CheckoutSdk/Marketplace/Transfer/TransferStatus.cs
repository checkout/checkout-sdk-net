using System.Runtime.Serialization;

namespace Checkout.Marketplace.Transfer
{
    public enum TransferStatus
    {
        [EnumMember(Value = "pending")] Pending,
        [EnumMember(Value = "completed")] Completed,
        [EnumMember(Value = "rejected")] Rejected
    }
}