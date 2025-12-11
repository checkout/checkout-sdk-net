using System.Runtime.Serialization;

namespace Checkout.Payments.Sessions
{
    public enum StorePaymentDetailsType
    {
        [EnumMember(Value = "disabled")] Disabled,
        [EnumMember(Value = "enabled")] Enabled,
        [EnumMember(Value = "collect_consent")] CollectConsent
    }
}