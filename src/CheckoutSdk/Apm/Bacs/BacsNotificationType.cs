using System.Runtime.Serialization;

namespace Checkout.Apm.Bacs
{
    /// <summary>
    /// The type of pre-notification being sent to the payer.
    /// </summary>
    public enum BacsNotificationType
    {
        [EnumMember(Value = "advance_notice")]
        AdvanceNotice,
    }
}
