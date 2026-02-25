using Checkout.Issuing.Common;

namespace Checkout.Issuing.ControlGroups.Common
{
    /// <summary>
    /// Control group control that applies velocity limits to restrict transaction frequency and amounts over time periods.
    /// Note: Control profiles cannot be a target for velocity_limit controls.
    /// </summary>
    public class VelocityControlGroupControl : ControlGroupControl
    {
        /// <summary>
        /// Initializes a new instance of the VelocityControlGroupControl class.
        /// </summary>
        public VelocityControlGroupControl() : base(IssuingControlType.VelocityLimit)
        {
        }

        /// <summary>
        /// The velocity limit configuration that defines transaction frequency and amount restrictions over time periods.
        /// [Required]
        /// </summary>
        public VelocityLimit VelocityLimit { get; set; }
    }
}