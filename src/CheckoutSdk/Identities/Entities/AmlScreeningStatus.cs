using System.Runtime.Serialization;

namespace Checkout.Identities.Entities
{
    public enum AmlScreeningStatus
    {
        [EnumMember(Value = "created")]
        Created,
        [EnumMember(Value = "screening_in_progress")]
        ScreeningInProgress,
        [EnumMember(Value = "approved")]
        Approved,
        [EnumMember(Value = "declined")]
        Declined,
        [EnumMember(Value = "review_required")]
        ReviewRequired
    }
}