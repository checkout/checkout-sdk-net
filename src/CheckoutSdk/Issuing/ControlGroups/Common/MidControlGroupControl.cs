using Checkout.Issuing.Common;

namespace Checkout.Issuing.ControlGroups.Common
{
    /// <summary>
    /// Control group control that applies MID (Merchant Identifier) limits to restrict or allow transactions at specific merchants.
    /// </summary>
    public class MidControlGroupControl : ControlGroupControl
    {
        /// <summary>
        /// Initializes a new instance of the MidControlGroupControl class.
        /// </summary>
        public MidControlGroupControl() : base(IssuingControlType.MidLimit)
        {
        }

        /// <summary>
        /// The MID limit configuration that defines which merchant identifiers to block or allow.
        /// [Required]
        /// </summary>
        public MidLimit MidLimit { get; set; }
    }
}