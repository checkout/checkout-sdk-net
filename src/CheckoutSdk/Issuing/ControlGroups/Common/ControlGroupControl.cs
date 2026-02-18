using Checkout.Issuing.Common;

namespace Checkout.Issuing.ControlGroups.Common
{
    /// <summary>
    /// Base class for control group controls that define restrictions or permissions for card usage.
    /// </summary>
    public abstract class ControlGroupControl
    {
        /// <summary>
        /// The type of control being applied.
        /// Enum: "mcc_limit" "mid_limit" "velocity_limit"
        /// [Required]
        /// </summary>
        public IssuingControlType? ControlType { get; set; }

        /// <summary>
        /// A description of the control.
        /// [Optional]
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the ControlGroupControl class with the specified control type.
        /// </summary>
        /// <param name="controlType">The type of control</param>
        protected ControlGroupControl(IssuingControlType controlType)
        {
            ControlType = controlType;
        }
    }
}