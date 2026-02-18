using Checkout.Common;
using System;

namespace Checkout.Issuing.Common.Responses
{
    /// <summary>
    /// Base class for Issuing entity responses that contain common properties like ID, creation date, and last modified date.
    /// </summary>
    public abstract class ControlIssuingResponseBase : Resource
    {
        /// <summary>
        /// The entity's unique identifier.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The date and time when the entity was created.
        /// &lt;date-time&gt;
        /// [Required]
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// The date and time when the entity was last modified.
        /// &lt;date-time&gt;
        /// [Required]
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }
    }
}