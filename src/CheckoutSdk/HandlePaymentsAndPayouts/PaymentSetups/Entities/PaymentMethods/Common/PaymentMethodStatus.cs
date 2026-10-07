using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The payment method status.
    /// Enum: "unavailable" "action_required" "ready" "initialization_required" "invalid"
    /// </summary>
    public enum PaymentMethodStatus
    {
        /// <summary>
        /// Not defined by the current API specification. Kept for backward compatibility.
        /// </summary>
        [EnumMember(Value = "available")]
        Available,

        /// <summary>
        /// Not defined by the current API specification; the API returns action_required. Kept for
        /// backward compatibility.
        /// </summary>
        [EnumMember(Value = "requires_action")]
        RequiresAction,

        /// <summary>
        /// The payment method is not available for this payment setup.
        /// </summary>
        [EnumMember(Value = "unavailable")]
        Unavailable,

        /// <summary>
        /// The payment method needs an action before it can be confirmed, for example a redirect.
        /// </summary>
        [EnumMember(Value = "action_required")]
        ActionRequired,

        /// <summary>
        /// The payment method is ready to be confirmed.
        /// </summary>
        [EnumMember(Value = "ready")]
        Ready,

        /// <summary>
        /// The payment method must be initialized, by setting its initialization to enabled, before
        /// it can be used.
        /// </summary>
        [EnumMember(Value = "initialization_required")]
        InitializationRequired,

        /// <summary>
        /// The payment method details are invalid. The flags describe what is missing or wrong.
        /// </summary>
        [EnumMember(Value = "invalid")]
        Invalid
    }
}
