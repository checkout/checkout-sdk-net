using Checkout.Issuing.Common;

namespace Checkout.Issuing.ControlGroups.Common
{
    /// <summary>
    /// Control group control that applies MCC (Merchant Category Code) limits to restrict or allow transactions based on merchant categories.
    /// </summary>
    public class MccControlGroupControl : ControlGroupControl
    {
        /// <summary>
        /// Initializes a new instance of the MccControlGroupControl class.
        /// </summary>
        public MccControlGroupControl() : base(IssuingControlType.MccLimit)
        {
        }

        /// <summary>
        /// The MCC limit configuration that defines which merchant category codes to block or allow.
        /// [Required]
        /// </summary>
        public MccLimit MccLimit { get; set; }
    }
}