using Checkout.Issuing.Common;
using System;

namespace Checkout.Issuing.Cards.Responses.Create
{
    /// <summary>
    /// A control that was set on the card when the virtual card was created, discriminated on control_type.
    /// </summary>
    public abstract class AbstractCardControlsResponse
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
        /// The ID of the card the control applies to.
        /// Not returned by the API in this response; still deserialized if present, otherwise null.
        /// [Optional]
        /// </summary>
        [Obsolete("Not returned by the API in this response. Will be removed in the next major version.")]
        public string TargetId { get; set; }

        /// <summary>
        /// A description for the control.
        /// [Required]
        /// max 256 characters
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The date and time the control was created.
        /// Not returned by the API in this response; still deserialized if present, otherwise null.
        /// [Optional]
        /// </summary>
        [Obsolete("Not returned by the API in this response. Will be removed in the next major version.")]
        public string CreatedDate { get; set; }

        /// <summary>
        /// The date and time the control was last modified.
        /// Not returned by the API in this response; still deserialized if present, otherwise null.
        /// [Optional]
        /// </summary>
        [Obsolete("Not returned by the API in this response. Will be removed in the next major version.")]
        public string LastModifiedDate { get; set; }

        /// <summary>
        /// Initializes the control with its discriminator value.
        /// </summary>
        /// <param name="controlType">The control's type.</param>
        protected AbstractCardControlsResponse(IssuingControlType controlType)
        {
            ControlType = controlType;
        }
    }
}
