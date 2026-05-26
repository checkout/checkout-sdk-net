using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Requirements
{
    public enum EntityRequirementReason
    {
        [EnumMember(Value = "periodic_review")]
        PeriodicReview,

        [EnumMember(Value = "attestation")]
        Attestation
    }
}
