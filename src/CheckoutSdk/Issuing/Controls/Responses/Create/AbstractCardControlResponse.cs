using Checkout.Common;
using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Responses.Create
{
    /// <summary>
    /// A card control returned by the create, get, list and update control operations,
    /// discriminated on control_type.
    /// Links (_links, ControlLinks with the self link to the control's details) are returned by the create
    /// operation only; get, list and update leave Links empty.
    /// </summary>
    public abstract class AbstractCardControlResponse : Resource
    {
        /// <summary>
        /// The control's type.
        /// A velocity_limit determines how much can be spent over a given period of time. An mcc_limit determines
        /// the types of businesses from which transactions can be processed. A mid_limit specifies the merchants
        /// from whom transactions can be processed.
        /// [Required]
        /// </summary>
        public IssuingControlType? ControlType { get; set; }

        /// <summary>
        /// The control's unique identifier.
        /// [Required]
        /// ^ctr_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The ID of the card or control profile.
        /// [Required]
        /// ^(crd|cpr)_[a-z0-9]{26}$
        /// 30 characters
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Indicates whether you can change this control.
        /// false: an immutable control applied by Checkout.com that you cannot change.
        /// true: you applied this control and can change it.
        /// [Required]
        /// </summary>
        public bool? IsEditable { get; set; }

        /// <summary>
        /// The date and time the control was created.
        /// [Required]
        /// Format: UTC date-time
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// The date and time the control was last modified.
        /// [Required]
        /// Format: UTC date-time
        /// </summary>
        public string LastModifiedDate { get; set; }

        /// <summary>
        /// The description of the control.
        /// [Optional]
        /// max 256 characters
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes the control with its discriminator value.
        /// </summary>
        /// <param name="controlType">The control's type.</param>
        protected AbstractCardControlResponse(IssuingControlType controlType)
        {
            ControlType = controlType;
        }
    }
}
