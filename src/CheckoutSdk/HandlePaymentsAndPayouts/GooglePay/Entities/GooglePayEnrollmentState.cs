using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay.Entities
{
    /// <summary>
    /// The current enrollment state of the entity with Google Pay.
    /// </summary>
    public enum GooglePayEnrollmentState
    {
        [EnumMember(Value = "ACTIVE")] Active,
        [EnumMember(Value = "INACTIVE")] Inactive,
    }
}
