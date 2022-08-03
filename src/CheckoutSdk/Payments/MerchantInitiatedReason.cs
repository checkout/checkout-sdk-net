using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum MerchantInitiatedReason
    {
        [EnumMember(Value = "Delayed_charge")] DelayedCharge,
        [EnumMember(Value = "Resubmission")] Resubmission,
        [EnumMember(Value = "No_show")] NoShow,
        [EnumMember(Value = "Reauthorization")] Reauthorization
    }
}